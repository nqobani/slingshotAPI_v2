using Slingshot.Data.EntityFramework;
using Slingshot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot.Data
{
    public class DbConnection
    {
        ApplicationDbContext dbCon = new ApplicationDbContext();

        public User createUser(string email, string password, string type)
        {
            User newUser = new User();
            newUser.email = email;
            newUser.password = password;
            newUser.type = type;
            dbCon.tblUsers.Add(newUser);

            dbCon.SaveChanges();
            long userId = newUser.Id;

            User user = dbCon.tblUsers.SingleOrDefault(u => u.Id == userId);
            return user;
        }
        public VCard createVCard(long userId, string firstName, string lastName, string company, string jobTitle, string email, string webPageAddress, string twitter, string businessPhoneNumber, string mobilePhone, string country, string city, string cityCode, string imageLink)
        {
            VCard newVCard = new VCard();
            newVCard.userId = userId;
            newVCard.firstName = firstName;
            newVCard.lastName = lastName;
            newVCard.company = company;
            newVCard.jobTitle = jobTitle;
            newVCard.email = email;
            newVCard.webPageAddress = webPageAddress;
            newVCard.twitter = twitter;
            newVCard.businessPhoneNumber = businessPhoneNumber;
            newVCard.mobileNumber = mobilePhone;
            newVCard.country = country;
            newVCard.city = city;
            newVCard.code = cityCode;
            newVCard.profileImage = imageLink;

            dbCon.tblVCards.Add(newVCard);
            dbCon.SaveChanges();

            long vCardId = newVCard.Id;

            VCard vcard = dbCon.tblVCards.FirstOrDefault(v => v.Id == vCardId);
            return vcard;

        }
        public void userCampaign(long userId, long campaignId)
        {
            UserCampaign usercampaign = new UserCampaign();
            usercampaign.campaignId = campaignId;
            usercampaign.userId = userId;

            dbCon.tblUserCampaigns.Add(usercampaign);
            dbCon.SaveChanges();
        }



        public Campaign createCampaign(long creatorId, string name, string thumbnail, string status = "public")
        {
            Campaign newCampaign = new Campaign();
            newCampaign.creatorId = creatorId;
            newCampaign.name = name;
            newCampaign.thumbnail = thumbnail;
            newCampaign.status = status;
            dbCon.tblCampaigns.Add(newCampaign);
            dbCon.SaveChanges();

            long campaignId = newCampaign.Id;
            Campaign campaign = dbCon.tblCampaigns.SingleOrDefault(c => c.Id == campaignId);
            return campaign;
        }
        public Email createEmail(long campaignId, string subject, string HTML)
        {
            Email email = new Email();
            email.campaignId = campaignId;
            email.subject = subject;
            email.html = HTML;

            dbCon.tblEmails.Add(email);
            dbCon.SaveChanges();

            long emailId = email.Id;
            Email mail = dbCon.tblEmails.FirstOrDefault(em => em.Id == emailId);
            return mail;
        }
        public Attachment createAttachment(long emailId, string name, string file/*Path*/)
        {
            Attachment newAttechment = new Attachment();
            newAttechment.emailId = emailId;
            newAttechment.name = name;
            newAttechment.file = file;
            dbCon.tblAttachments.Add(newAttechment);
            dbCon.SaveChanges();

            long attId = newAttechment.Id;
            Attachment attachment = dbCon.tblAttachments.SingleOrDefault(a => a.Id == attId);
            return attachment;
        }

        public string GetUserEmail(long userId)
        {
            User user = dbCon.tblUsers.FirstOrDefault(s => s.Id == userId);
            string email = user.email;
            return email;
        }
        public VCard GetVCard(long vCardId)
        {
            VCard vcard = dbCon.tblVCards.FirstOrDefault(v => v.Id == vCardId);
            return vcard;
        }
        public Email GetEmail(long campId)
        {
            var email = dbCon.tblEmails.FirstOrDefault(e => e.campaignId == campId);
            return email;
        }
        


        public IEnumerable<Campaign> getAllCampaigns(long userId, string campName)
        {
            var camps = dbCon.tblCampaigns.Where(c => c.creatorId == userId||c.status.ToLower().Contains("public"));
            return camps;
        }
        public IEnumerable<Attachment> GetAttachmentByEmailId(long emailId)
        {
            var atts = dbCon.tblAttachments.Where(s => s.emailId == emailId);
            return atts;
        }
        public IEnumerable<History> GetUserHistory(long userId)
        {
            var history = dbCon.tblHistory.Where(h=>h.userId==userId);
            return history;
        }
        public IEnumerable<VCard> GetVCards(long userId)
        {
            var vCards = dbCon.tblVCards.Where(u => u.userId == userId);
            return vCards;
        }

        public History createHistory(long userId, long campaignId, string toEMail, long imageId = 0)
        {
            History newHistory = new History();
            newHistory.userId = userId;
            newHistory.imageId = imageId;
            newHistory.campaignId = campaignId;
            newHistory.toEmail = toEMail;
            newHistory.sentDateTime = DateTime.Now;

            dbCon.tblHistory.Add(newHistory);
            dbCon.SaveChanges();

            long histId = newHistory.Id;
            History history = dbCon.tblHistory.FirstOrDefault(h => h.Id == histId);
            return history;
        }

    }
}
