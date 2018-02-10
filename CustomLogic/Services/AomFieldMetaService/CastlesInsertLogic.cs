using System;
using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Models;

namespace CustomLogic.Services.AomFieldMetaService
{
    public class Save : IInsertEvent<AomFieldMetaViewModel>
    {
        public Guid CreatedId = Guid.NewGuid();

        public bool Run(AomFieldMetaViewModel model, IUnitOfWork unitOfWork, Response<AomFieldMetaViewModel> result)
        {
          
            var newCustom = AomFieldMetaMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<AomFieldMeta>().Add(newCustom);
            unitOfWork.SaveChanges();
            CreatedId = newCustom.Id;
            var newCustomResult = AomFieldMetaMapper.MapDbModelToViewModel(newCustom);
            result.Data = newCustomResult;
            return true;
        }

        public bool Rollback(AomFieldMetaViewModel model, IUnitOfWork unitOfWork)
        {
            var removeItem = unitOfWork.With<AomFieldMeta>().FirstOrDefault(c=>c.Id == CreatedId);
            unitOfWork.With<AomFieldMeta>().Remove(removeItem);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}