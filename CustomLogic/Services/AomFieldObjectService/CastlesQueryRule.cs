
using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.BL;
using CustomLogic.Core.Models;

namespace CustomLogic.Services.AomFieldObjectService
{
    public class LimitAomFieldObjects : IViewListRule<AomFieldObjectViewModel, AomFieldObject>
    {
      
        public bool Run(NgTableParams model, ref IQueryable<AomFieldObject> repository, NgTable<AomFieldObjectViewModel> result, ICoreUser user, IUnitOfWork unitOfWork)
        {
             // unitOfWork = unitOfWork.Where(c => c.OwnerId == null);
             // limit by organisation owner etc... business rules 
              return true;
        }

    }
}

