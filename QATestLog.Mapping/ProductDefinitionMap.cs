using FluentNHibernate.Mapping;

namespace QATestLog.Mapping
{
	public class ProductDefinitionMap : ClassMap<ProductDefinition>
	{
		public ProductDefinitionMap()
		{
			Id(pmli => pmli.Id)
				.GeneratedBy
				.Assigned();
			Map(pmli => pmli.Name);
			Map(pmli => pmli.Description);
		}
	}
}