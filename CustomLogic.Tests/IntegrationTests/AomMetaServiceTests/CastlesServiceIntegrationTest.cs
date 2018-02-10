using System.Diagnostics;
using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Models;
using CustomLogic.Services.AomMetaService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CustomLogic.Tests.IntegrationTests.AomMetaServiceTests
{
    [TestClass]
    public class AomMetaServiceTests
    {
        
        public AomMetaServiceTests()
        {
           // mockUser = Implement your mockUserHere
        }

        private ICoreUser mockUser;

        private readonly IService<AomMetaViewModel> _AomMetaService = new AomMetaService(new AomDbContext());


        public Response<AomMetaViewModel> AomMetaResponse { get; set; }

        [TestMethod]
        public void Insert()
        {
            // Arrange - Create a new account view model
            var newAomMetaViewModel = new AomMetaViewModel()
            {
                Name = "New Aom Meta Name"
            };


            // Act - send this to the insert method on the account service logic
            var Response = _AomMetaService.Insert(newAomMetaViewModel, mockUser);


            // Assert
            Assert.IsTrue(Response.Success);

        }

        [TestMethod]
        public void View()
        {
            // Arrange - insert a deal so that we can pull it out
            var result1 = _AomMetaService.Insert(new AomMetaViewModel() {Name = "Test"}, mockUser);

            // Act
            var result = _AomMetaService.View(new AomMetaViewModel { ID = result1.Data.ID } , mockUser);

            // Assert
            Assert.IsTrue(result.Success);

        }

        [TestMethod]
        public void Update()
        {
            // Arrange - insert a deal so that we have something to edit
            var result2 = _AomMetaService.Insert(new AomMetaViewModel()
            {
                Name = "Test Delate me"
            }
            , mockUser);

            // Act
            result2.Data.Name = "Changed";
            var result = _AomMetaService.Update(result2.Data, mockUser);


            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Name == result2.Data.Name);

        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var result4 = _AomMetaService.Insert(new AomMetaViewModel()
            {
                Name = "Test AomMeta which i will delete"
            }
            , mockUser);

            // Act
            var result = _AomMetaService.Delete(new AomMetaViewModel() 
             { ID = result4.Data.ID }
            , mockUser);

            var result2 = _AomMetaService.View(new AomMetaViewModel()
             { ID = result4.Data.ID }
            , mockUser);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(!result2.Success);
        }


        [TestMethod]
        public void Query()
        {

            // Arrange - insert a deal so that we have something to edit
            object filter = new { Id = "1" };

#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            // Act
            var result = _AomMetaService.List(new NgTableParams
            {
                filter = JsonConvert.SerializeObject(filter)
            }, mockUser);

#if DEBUG
            sw.Stop();
            var totalTime = sw.Elapsed.Seconds;
#endif

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Any());
            
        }

    }
}
