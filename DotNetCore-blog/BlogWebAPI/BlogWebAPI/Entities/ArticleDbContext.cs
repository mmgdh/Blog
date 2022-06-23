using Ariticle.Domain;
using CommonInfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ariticle.Entities
{
    public class ArticleDbContext : BaseDbContext
    {
        public ArticleDbContext(DbContextOptions options, IMediator? mediator) : base(options, mediator)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
