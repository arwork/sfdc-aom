using CustomLogic;
using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;

namespace CustomLogic.Services.AomFieldObjectService
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api  
    /// </summary>
    public class AomFieldObjectService : ServiceBase<AomFieldObjectViewModel, AomFieldObject>
    {
        public AomFieldObjectService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

