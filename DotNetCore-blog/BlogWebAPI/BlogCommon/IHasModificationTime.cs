using System;

namespace DomainCommon
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; }

    }
}
