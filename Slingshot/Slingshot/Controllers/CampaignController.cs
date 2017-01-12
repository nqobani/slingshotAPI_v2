﻿using Slingshot.Data.Models;
using Slingshot.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Slingshot.Controllers
{
    [RoutePrefix("api/campaign")]
    public class CampaignController : ApiController
    {
        UserService obj = new UserService();
        [Route("send")]
        public History sendCampaigns(long userId, long vcardId, long campId, string toEmail)
        {
            return obj.sendCampaign(userId, vcardId, campId, toEmail);
        }
        [Route("add")]
        public Campaign addCampaign(int creatorId, string attechmentsJSONString, string campaignName = "No Name", string thumbnail = "HTTPS", string subject = "TESTIING", string HTML = "<!DOCTYPE html>", string status = "public")
        {
            UserService obj = new UserService();
            return obj.createCampaign(creatorId, campaignName, thumbnail, subject, HTML, attechmentsJSONString, status);
        }
        [Route("get")]
        public IEnumerable<Campaign> getCampaigns(long userId, string name = "")
        {
            UserService obj = new UserService();
            return obj.getCampaigns(userId, name);
        }
        [Route("share")]
        public Boolean shareCampaign(long userId, long campaignId)
        {
            return obj.ShareCampaigns(userId, campaignId);
        }
        [Route("uploadImage")]
        public void uploadImage()
        {
            Directory.CreateDirectory(@"C:\Users\User\Music\images");
            string sourceFile = Path.Combine(@"C:\Users\User\Music\", "banner.jpg");
            string destFile = Path.Combine(@"C:\Users\User\Music\images\", "banner.jpg");
            File.Copy(sourceFile, destFile, true);
        }
    }
}
