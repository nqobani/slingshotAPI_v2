using SendGrid.Helpers.Mail;
using Slingshot.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
