using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QATestLog
{
    public class ProductDefinition
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public  ProductDefinition()
        {
        }

		public ProductDefinition(Guid newGuid, string newName, string newDescription)
        {
            Id = newGuid;
            Name = newName;
            Description = newDescription;
        }
    }
}
