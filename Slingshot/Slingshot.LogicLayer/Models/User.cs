using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot.Data.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string type { get; set; }
    }
}
