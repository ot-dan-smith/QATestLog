using System;
using System.Collections.Generic;

namespace QATestLog
{
    public class Product
    {
        public virtual Guid Id { get; set; }
		public virtual Guid BuildId { get; set; }
        public virtual Guid MasterProductListId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        
		public virtual IList<QATest> Tests { get; set; }

        public Product(Guid newGuid, Guid masterProductId,Guid buildId, string name, string description)
        {
            Id = newGuid;
            Name = name;
            Description = description;
			BuildId = buildId;
            MasterProductListId = masterProductId;

			Tests = new List<QATest>();
        }
        public Product()
        {
			Tests = new List<QATest>();
        }
    }
}
