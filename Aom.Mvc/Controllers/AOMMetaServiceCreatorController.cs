using System.Web.Mvc;
using CustomLogic;
using CustomLogic.Services.AOMMetaCreatorService;
using CustomLogic.Services.AOMMetaCreatorService.Connectors;

namespace Aom.Mvc.Controllers
{
    public class AOMMetaServiceCreatorController : Controller
    {
        // GET: AOMMetaServiceCreator
        public ActionResult Index()
        {
            return View(new ResetTablesModel
            {
                userName = "",
                password = "",
                tables = "Opportunity,Task,Account,Contact,OpportunityContactRole"
            });
        }

        [HttpPost]
        public ActionResult Index(ResetTablesModel mode )
        {
            var db = new AomDbContext();
            db.Database.ExecuteSqlCommand(@"  delete[dbo].[RelationshipObject]
        delete[dbo].[RelationshipMeta]
        delete[dbo].[AomFieldObject]
        delete[dbo].[AomObject]
        delete[dbo].[AomFieldMeta]
        delete[dbo].[FieldTypes]
        delete[dbo].[AomMeta]");

            AOMMetaCreatorService service = new AOMMetaCreatorService(new SalesforceConnector());
            var result = service.CreateTables(mode.userName, mode.password, mode.tables.Split(','));
            return View();
        }
    }

    public class ResetTablesModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string tables { get; set; }
    }
}