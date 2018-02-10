using CustomLogic;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Models;

namespace CustomLogic.Services.AomFieldObjectService
{
    public class Delete : IDeleteEvent<AomFieldObjectViewModel>
    {
        public bool Run(AomFieldObjectViewModel model, IUnitOfWork unitOfWork, Response<AomFieldObjectViewModel> result)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<AomFieldObject>().Find(model.ID);
            unitOfWork.With<AomFieldObject>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}
