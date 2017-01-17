using Slingshot.Data.Models;
using Slingshot.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Slingshot.Controllers
{
    [RoutePrefix("api/recipient")]
    public class RecipientController : ApiController
    {
        UserService obj = new UserService();
        [Route("addclient")]
        public Recipient AddClient(string userId, string fName, string lName, string email, string phone)
        {
            return obj.CaptureRecipient(userId, fName, lName, email, phone);
        }
        [Route("addVCard")]
        public ClientVCard CreateClientVCard(long clientId, string profilImage, string fName, string lName, string company, string jobTitle, string fileAs, string email, string twitter, string webPageAddress, string businessPhoneNumber, string mobileNumber, string country, string city, string code)
        {
            return obj.CreateClientVCard( clientId,  profilImage,  fName,  lName,  company,  jobTitle,  fileAs,  email,  twitter,  webPageAddress,  businessPhoneNumber,  mobileNumber,  country,  city,  code);
        }
        [Route("getUserClients")]
        public IEnumerable<Recipient> GetAllUserClients(long userId)
        {
            return obj.GetAllUserRecipients(userId);
        }
        [Route("getClient")]
        public Recipient GetClient(long userId)
        {
            return obj.GetRecipient(userId);
        }
        [Route("getVCards")]
        public IEnumerable<ClientVCard> GetClientVCards(long clientId)
        {
            return obj.GetClientVCards(clientId);
        }
        [Route("getVCard")]
        public ClientVCard GetClientVCard(long vCardId)
        {
            return obj.GetClientVCard(vCardId);
        }
    }
}
