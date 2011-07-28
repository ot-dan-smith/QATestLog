using FluentNHibernate.Mapping;

namespace QATestLog.Mapping
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(p => p.Id)
                .GeneratedBy
                .Assigned();
			Map(p => p.ProjectId,"ProjectId");
			Map(p => p.MasterProductListId, "MasterProductListId");
            Map(p => p.Name);
            Map(p => p.Description);

			HasMany(p => p.Builds).KeyColumn("BuildId").Inverse().Cascade.All(); ;
        }
    }
}
