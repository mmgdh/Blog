using System;

namespace DomainCommon
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; }
    }
}
