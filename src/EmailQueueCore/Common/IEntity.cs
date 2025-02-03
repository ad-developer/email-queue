using System;

namespace EmailQueueCore.Common;

public interface IEntity
{
    public Guid Id { get; set; }
}
