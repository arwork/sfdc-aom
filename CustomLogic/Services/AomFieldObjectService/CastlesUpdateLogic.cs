 using System.Data.Entity.Migrations;
 using CustomLogic;
 using CustomLogic.Core.Interfaces;
 using CustomLogic.Core.Interfaces.DL;
 using CustomLogic.Core.Models;
 using CustomLogic.Services.AomFieldMetaService;

namespace CustomLogic.Services.AomFieldObjectService
{
 public class Update : IUpdateEvent<AomFieldObjectViewModel>
    {
        public bool Run(AomFieldObjectViewModel model, IUnitOfWork unitOfWork, Response<AomFieldObjectViewModel> result)
        {
            var dbModel = unitOfWork.With<AomFieldObject>().Find(model.ID);
            var updatedDbModel = AomFieldObjectMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<AomFieldObject>().AddOrUpdate(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = AomFieldObjectMapper.MapDbModelToViewModel(updatedDbModel);
            result.Data = newCustomResult;
            return true;
        }
    }
}
