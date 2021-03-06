using System.Linq;
using CustomLogic;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Models;
using CustomLogic.Services.AomFieldMetaService;

namespace CustomLogic.Services.AomMetaService
{
    public class QueryLogic : IViewListEvent<AomMetaViewModel, AomMeta>
    {
        public bool Run(NgTableParams model, ref IQueryable<AomMeta> repository, NgTable<AomMetaViewModel> result, ICoreUser user, IUnitOfWork db)
        {
            var ngTransformer = new QueryToNgTable<AomMetaViewModel>();

            var query = AomMetaMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}
   