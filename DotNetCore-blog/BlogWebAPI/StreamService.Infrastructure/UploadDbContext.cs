using CommonInfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamService.Domain.Entities;

namespace StreamService.Infrastructure
{
    public class UploadDbContext : BaseDbContext
    {
        public DbSet<UploadItem> uploads { get; set; }
        public DbSet<UploadUri> urls { get; set; }

        public UploadDbContext(DbContextOptions options, IMediator? mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
