using CommonInfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BlogInfoService.Domain.Entities;

namespace BlogInfoService.Infrastructure
{
    public class BlogParamDbContext: BaseDbContext
    {

        public DbSet<BlogParameter> blogParameters { get; set; }
        public BlogParamDbContext(DbContextOptions options, IMediator? mediator) : base(options, mediator)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
