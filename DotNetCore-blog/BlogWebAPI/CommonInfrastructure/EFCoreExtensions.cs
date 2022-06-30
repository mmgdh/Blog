using DomainCommon;
using Microsoft.EntityFrameworkCore;

namespace CommonInfrastructure
{
    public static class EFCoreExtensions
    {
        public static IQueryable<T> Query<T>(this DbContext ctx) where T : class, IEntity
        {
            return ctx.Set<T>().AsNoTracking();
        }
    }
}
