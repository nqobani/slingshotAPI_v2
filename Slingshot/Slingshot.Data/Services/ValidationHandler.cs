using SendGrid.Helpers.Mail;
using Slingshot.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot.Data.Services
{
    public class ValidationHandler
    {
        private ApplicationDbContext con = new ApplicationDbContext();
        public Boolean UserExist(long userId)
        {
            var userC = con.tblUsers.SingleOrDefault(s => s.Id == userId);
            Boolean userExists = false;
            if (userC.Id == userId)
            {
                userExists = true;
            }
            return userExists;
        }

        public Boolean UserCampaignValidation(long useId, long campId)
        {
            var uc = con.tblUserCampaigns.FirstOrDefault(s => s.userId == useId && s.campaignId == campId);

            Boolean hasAccess = false;

            if (uc.campaignId == campId && useId == uc.userId)
            {
                hasAccess = true;
            }
            return hasAccess;
        }

        public SendGrid.Helpers.Mail.Attachment GetAttechmentData(string filePath)
        {
            if(filePath.Substring(0,8).ToLower().Contains("https"))
            {
                string base64ImageRepresentation = ConvertImageURLToBase64(filePath);
                string fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
                string type = filePath.Substring(filePath.LastIndexOf('.') + 1);

                return new SendGrid.Helpers.Mail.Attachment
                {
                    Filename = fileName,
                    Type = type,
                    Disposition = "inline",
                    ContentId = "kjhlknmnjhjkk",
                    Content = base64ImageRepresentation
                };
            }
            else
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                string type = filePath.Substring(filePath.LastIndexOf('.') + 1);

                return new SendGrid.Helpers.Mail.Attachment
                {
                    Filename = fileName,
                    Type = type,
                    Disposition = "inline",
                    ContentId = "kjhlknmnjhjkk",
                    Content = base64ImageRepresentation
                };
            }
            
        }
        public string GetUserType(long userId)
        {
            var user = con.tblUsers.FirstOrDefault(u => u.Id == userId);
            string userType = user.type;
            return userType;
        }
        public Boolean IsCraetor(long userId, long campaignId)
        {
            Boolean isCreator = false;
            var camp = con.tblCampaigns.SingleOrDefault(c => c.creatorId == userId && c.Id == campaignId);
            if (camp.Id == campaignId && camp.creatorId == userId)
            {
                isCreator = true;
            }
            return isCreator;
        }
        public Boolean CanUserShare(long userId, long campaignId)
        {
            Boolean share = false;
            string userType = GetUserType(userId);
            if (userType.ToLower().Equals("admin"))
            {
                share = true;
            }
            else
            {
                share = IsCraetor(userId, campaignId);
            }
            return share;
        }



        public String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = GetImage(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();
        }

        public byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }
    }
}
