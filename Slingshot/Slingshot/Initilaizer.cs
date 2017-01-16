using Slingshot.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Slingshot
{
    public class Initilaizer
    {
        public void MigrateToLatest()
        {
            var dbMigrator = new DbMigrator(new Configuration());
            if (dbMigrator.GetPendingMigrations().Any())
            {
                dbMigrator.Update();
            }
        }
    }
}