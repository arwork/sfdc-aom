using CustomLogic;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Models;

namespace CustomLogic.Services.AomMetaService
{
    public class Delete : IDeleteEvent<AomMetaViewModel>
    {
        public bool Run(AomMetaViewModel model, IUnitOfWork unitOfWork, Response<AomMetaViewModel> result)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<AomMeta>().Find(model.ID);
            unitOfWork.With<AomMeta>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}
