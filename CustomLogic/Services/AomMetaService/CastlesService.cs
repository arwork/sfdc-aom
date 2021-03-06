using CustomLogic;
using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;

namespace CustomLogic.Services.AomMetaService
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api  
    /// </summary>
    public class AomMetaService : ServiceBase<AomMetaViewModel, AomMeta>
    {
        public AomMetaService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

