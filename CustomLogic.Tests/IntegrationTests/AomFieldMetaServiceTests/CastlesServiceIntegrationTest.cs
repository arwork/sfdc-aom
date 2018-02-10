using System.Diagnostics;
using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Models;
using CustomLogic.Services.AomFieldMetaService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CustomLogic.Tests.IntegrationTests.AomFieldMetaServiceTests
{
    [TestClass]
    public class AomFieldMetaServiceTests
    {
        public AomFieldMetaServiceTests()
        {
           // mockUser = Implement your mockUserHere
        }

        private ICoreUser mockUser;

        private readonly IService<AomFieldMetaViewModel> _AomFieldMetaService = new AomFieldMetaService(new AomDbContext());

      

        public Response<AomFieldMetaViewModel> AomFieldMetaResponse { get; set; }

        [TestMethod]
        public void Insert()
        {

            // Requires AomMetaId

            // Arrange - Create a new account view model
            var newAomFieldMetaViewModel = new AomFieldMetaViewModel()
            {
                Name = "New Aom Field Meta"
            };


            // Act - send this to the insert method on the account service logic
            var Response = _AomFieldMetaService.Insert(newAomFieldMetaViewModel, mockUser);


            // Assert
            Assert.IsTrue(Response.Success);

        }

        [TestMethod]
        public void View()
        {
            // Arrange - insert a deal so that we can pull it out
            var result1 = _AomFieldMetaService.Insert(new AomFieldMetaViewModel(), mockUser);

            // Act
            var result = _AomFieldMetaService.View(new AomFieldMetaViewModel { ID = result1.Data.ID } , mockUser);

            // Assert
            Assert.IsTrue(result.Success);

        }

        [TestMethod]
        public void Update()
        {
            // Arrange - insert a deal so that we have something to edit
            var result2 = _AomFieldMetaService.Insert(new AomFieldMetaViewModel()
                //{
                //    Title = "Test AomFieldMeta 1"
                //}
                , null);

            // Act
            //result2.Data.Title = "Changed";
            var result = _AomFieldMetaService.Update(result2.Data, mockUser);


            // Assert
            Assert.IsTrue(result.Success);
            //Assert.IsTrue(result.Data.Title == result2.Data.Title);

        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var result4 = _AomFieldMetaService.Insert(new AomFieldMetaViewModel()
                //{
                //    Title = "Test AomFieldMeta which i will delete"
                //}
                , mockUser);

            // Act
            var result = _AomFieldMetaService.Delete(new AomFieldMetaViewModel() 
                    { ID = result4.Data.ID }
                , mockUser);

            var result2 = _AomFieldMetaService.View(new AomFieldMetaViewModel()
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
            var result = _AomFieldMetaService.List(new NgTableParams
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
