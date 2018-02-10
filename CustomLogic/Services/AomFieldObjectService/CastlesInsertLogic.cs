using System;
using System.Linq;
using CustomLogic;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Models;


namespace CustomLogic.Services.AomFieldObjectService
{
    public class Save : IInsertEvent<AomFieldObjectViewModel>
    {
        public Guid CreatedId = Guid.NewGuid();

        public bool Run(AomFieldObjectViewModel model, IUnitOfWork unitOfWork, Response<AomFieldObjectViewModel> result)
        {
          
            var newCustom = AomFieldObjectMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<AomFieldObject>().Add(newCustom);
            unitOfWork.SaveChanges();
            CreatedId = newCustom.Id;
            var newCustomResult = AomFieldObjectMapper.MapDbModelToViewModel(newCustom);
            result.Data = newCustomResult;
            return true;
        }

        public bool Rollback(AomFieldObjectViewModel model, IUnitOfWork unitOfWork)
        {
            var removeItem = unitOfWork.With<AomFieldObject>().FirstOrDefault(c=>c.Id == CreatedId);
            unitOfWork.With<AomFieldObject>().Remove(removeItem);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}