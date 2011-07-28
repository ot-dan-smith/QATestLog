using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QATestLog
{
    public class QATestDefinition
    {
        public virtual Guid Id { get; set; }
        public virtual Guid MasterProductListId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Boolean isActive { get; set; }


        public  QATestDefinition()
        {
        }

        public QATestDefinition(Guid newGuid,Guid productId, string newName, string newDescription)
        {
            Id = newGuid;
            MasterProductListId = productId;
            Name = newName;
            Description = newDescription;
            isActive = true;

        }
    }
}
