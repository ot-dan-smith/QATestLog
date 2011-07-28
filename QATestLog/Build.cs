using System;
using System.Collections.Generic;

namespace QATestLog
{
    public class Build
    {
        public virtual Guid Id { get; set; }
		public virtual Guid ProjectId { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }

		public virtual IList<Product> Products { get; set; }

        public Build(Guid newGuid,Guid projectId, string name, string description)
        {
            Id = newGuid;
			ProjectId = projectId;
            Name = name;
            Description = description;

			Products = new List<Product>();
        }
        public Build()
        {
			Products = new List<Product>();
        }


    }
}