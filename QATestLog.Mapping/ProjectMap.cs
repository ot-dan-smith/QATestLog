using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;

namespace QATestLog.Mapping
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(p => p.Id)
                .GeneratedBy
                .Assigned();
            Map(p => p.Name);
            Map(p => p.Description);
            Map(p => p.CreatedOn);

            HasMany(p => p.Products).KeyColumn("ProjectId").Inverse().Cascade.All();
        }
    }
}
