using System;
using System.Linq;
using CustomLogic;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Models;


namespace CustomLogic.Services.AomMetaService
{
    public class Save : IInsertEvent<AomMetaViewModel>
    {
        public Guid CreatedId = Guid.NewGuid();

        public bool Run(AomMetaViewModel model, IUnitOfWork unitOfWork, Response<AomMetaViewModel> result)
        {
          
            var newCustom = AomMetaMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<AomMeta>().Add(newCustom);
            unitOfWork.SaveChanges();
            CreatedId = newCustom.Id;
            var newCustomResult = AomMetaMapper.MapDbModelToViewModel(newCustom);
            result.Data = newCustomResult;


            result.Messages.Add(new Message {MessageText = "You did it"});
            return true;
        }

        public bool Rollback(AomMetaViewModel model, IUnitOfWork unitOfWork)
        {
            var removeItem = unitOfWork.With<AomMeta>().FirstOrDefault(c=>c.Id == CreatedId);
            unitOfWork.With<AomMeta>().Remove(removeItem);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}