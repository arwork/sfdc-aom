    using CustomLogic;
    using CustomLogic.Core.Interfaces;
    using CustomLogic.Core.Interfaces.DL;
    using CustomLogic.Core.Models;

public class Delete : IDeleteEvent<AomFieldMetaViewModel>
    {
        public bool Run(AomFieldMetaViewModel model, IUnitOfWork unitOfWork, Response<AomFieldMetaViewModel> result)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<AomFieldMeta>().Find(model.ID);
            unitOfWork.With<AomFieldMeta>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
