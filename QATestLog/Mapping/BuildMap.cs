using FluentNHibernate.Mapping;

namespace QATestLog.Mapping
{
    public class BuildMap : ClassMap<Build>
    {
        public BuildMap()
        {
            Id(b => b.Id)
                .GeneratedBy
                .Assigned();
        	Map(b => b.ProjectId);
            Map(b => b.Name);
            Map(b => b.Description);

			HasMany(b => b.Products).KeyColumn("ProductId").Inverse().Cascade.All(); ;
        }
    }
}
