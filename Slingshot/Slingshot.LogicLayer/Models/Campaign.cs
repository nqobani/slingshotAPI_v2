﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot.Data.Models
{
    public class Campaign
    {
        [Key]
        public long Id { get; set; }
        public long creatorId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string thumbnail { get; set; }
        public ISet<Email> email { get; set; }
    }
}
