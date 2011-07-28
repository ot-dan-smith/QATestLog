using FluentNHibernate.Mapping;

namespace QATestLog.Mapping
{
	public class QATestDefinitionMap : ClassMap<QATestDefinition>
	{
		public QATestDefinitionMap()
		{
			Id(tmli => tmli.Id)
				.GeneratedBy
				.Assigned();
			Map(tmli => tmli.Name);
			Map(tmli => tmli.Description);
		}
	}
}
