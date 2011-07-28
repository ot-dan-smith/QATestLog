﻿using FluentNHibernate.Mapping;

namespace QATestLog.Mapping
{
    public class QATestMap : ClassMap<QATest>
    {
        public QATestMap()
        {
            Id(t => t.Id)
                .GeneratedBy
                .Assigned();
        	Map(t => t.ProductId);
        	Map(t => t.MasterTestListId);
            Map(t => t.Name);
            Map(t => t.Description);
            Map(t => t.Status);
            Map(t => t.Notes);
        }
    }
}
