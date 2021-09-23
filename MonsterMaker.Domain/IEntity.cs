using System;

namespace MonsterMaker.Domain
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime LastModified { get; set; }
    }
}
