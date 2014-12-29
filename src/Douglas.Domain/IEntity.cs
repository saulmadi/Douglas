using System;

namespace Douglas.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}