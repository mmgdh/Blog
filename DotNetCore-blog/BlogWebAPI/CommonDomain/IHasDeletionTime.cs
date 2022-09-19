using System;

namespace DomainCommon
{
    public interface IHasDeletionTime
    {
        DateTime? DeletionTime { get; }
    }
}
