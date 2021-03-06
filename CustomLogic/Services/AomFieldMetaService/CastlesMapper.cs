using System.Linq;

namespace CustomLogic.Services.AomFieldMetaService
{
    public static class AomFieldMetaMapper
    {


        public static AomFieldMeta MapInsertModelToDbModel(AomFieldMetaViewModel model, AomFieldMeta newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new AomFieldMeta();
            }

            newDomainModel.Id = model.ID;
            newDomainModel.AomMetaId = model.AomMetaId;
            newDomainModel.Name = model.Name;
            newDomainModel.FieldTypeId = model.FieldTypeId;

            return newDomainModel;
        }



        public static AomFieldMetaViewModel MapDbModelToViewModel(AomFieldMeta dbModel)
        {
            var viewModel = new  AomFieldMetaViewModel();
            viewModel.ID = dbModel.Id;
            viewModel.AomMetaId = dbModel.AomMetaId;
            viewModel.Name = dbModel.Name;
            viewModel.FieldTypeId = dbModel.FieldTypeId;
            return viewModel;
        }

        public static IQueryable<AomFieldMetaViewModel> MapDbModelQueryToViewModelQuery(IQueryable<AomFieldMeta> databaseQuery)
        {
            return databaseQuery.OrderByDescending(c=>c.Id).Select(c => new AomFieldMetaViewModel()
            {
                ID = c.Id,
                AomMetaId = c.AomMetaId,
                Name = c.Name,
                FieldTypeId = c.FieldTypeId,
            });
        }




    }
}

