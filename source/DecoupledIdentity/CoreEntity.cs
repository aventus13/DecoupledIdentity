using System;

namespace DecoupledIdentity.Core
{
    public abstract class CoreEntity
    {
        protected CoreEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }

        public string Id { get; }
        public DateTime CreatedDate { get; }
        public DateTime? DeletedDate { get; private set; }

        public void Delete()
        {
            if (DeletedDate.HasValue)
                return;

            DeletedDate = DateTime.Now;
        }
    }
}
