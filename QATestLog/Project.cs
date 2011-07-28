using System;
using System.Collections.Generic;

namespace QATestLog
{
    public class Project
    {
        public virtual Guid Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime CreatedOn { get; set; }

		public virtual IList<Build> Builds { get; set; }

        public Project(Guid id, string name, string description, DateTime createdOn)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedOn = createdOn;

			Builds = new List<Build>();
        }
        public Project()
        {
			Builds = new List<Build>();
        }
    }
}
