using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogic.Tests.IntegrationTests.CleanScrips
{
    public class ClearDatabase
    {
        public ClearDatabase()
        {
            var db = new AomDbContext();
            db.Database.ExecuteSqlCommand(@"  delete[dbo].[RelationshipObject]
        delete[dbo].[RelationshipMeta]
        delete[dbo].[AomFieldObject]
        delete[dbo].[AomObject]
        delete[dbo].[AomFieldMeta]
        delete[dbo].[AomMeta]");

        }
      

    }
}
