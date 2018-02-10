using System;
using System.Linq;
using CustomLogic.Services.AOMMetaCreatorService;
using CustomLogic.Services.AOMMetaCreatorService.Connectors;
using CustomLogic.Tests.IntegrationTests.CleanScrips;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomLogic.Tests.IntegrationTests.SalesforceTests
{
    [TestClass]
    public class SalesforceCreateAOMMeta : ClearDatabase
    {
        
        [TestMethod]
        public void SalesForceCanCreateTables()
        {
            AOMMetaCreatorService metaCreatorService = new AOMMetaCreatorService(new SalesforceConnector());
            var results = metaCreatorService.CreateTables("test@yopmail.com", "test", new string[] { "Opportunity", "Task", "Account", "OpportunityContactRole", "Contact" });
            Assert.IsTrue(results.Success);
            Assert.IsTrue(results.Data.Any());
        }
    }
}
