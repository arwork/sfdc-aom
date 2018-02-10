 using System.Data.Entity.Migrations;
 using CustomLogic;
 using CustomLogic.Core.Interfaces;
 using CustomLogic.Core.Interfaces.DL;
 using CustomLogic.Core.Models;
 using CustomLogic.Services.AomFieldMetaService;

namespace CustomLogic.Services.AomMetaService
{
 public class Update : IUpdateEvent<AomMetaViewModel>
    {
        public bool Run(AomMetaViewModel model, IUnitOfWork unitOfWork, Response<AomMetaViewModel> result)
        {
            var dbModel = unitOfWork.With<AomMeta>().Find(model.ID);
            var updatedDbModel = AomMetaMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<AomMeta>().AddOrUpdate(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = AomMetaMapper.MapDbModelToViewModel(updatedDbModel);
            result.Data = newCustomResult;
            return true;
        }
    }
}
