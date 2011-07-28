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

            HasMany(p => p.Builds).KeyColumn("BuildId").Inverse().Cascade.All();
        }
    }
}
