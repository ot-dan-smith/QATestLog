using System;

namespace QATestLog
{
    public class QATest
    {
        public QATest(Guid newGuid, Guid masterTestListID, Guid productId, string name, string description, int status, string notes)
        {
            Id = newGuid;
            MasterTestListId = masterTestListID;
			ProductId = productId;
            Name = name;
            Description = description;
            Status = status;
            Notes = notes;
        }

        public QATest()
        {}

        public virtual Guid Id { get; set; }
        public virtual Guid MasterTestListId {get;set;}
		public virtual Guid ProductId { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual int Status { get; set; }
        public virtual String Notes { get; set; }
    }
}
