using ArticleService.Domain.Entities;
using CommonInfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Infrastructure
{
    public class ArticleDbContext : BaseDbContext
    {
        public ArticleDbContext(DbContextOptions options, IMediator? mediator) : base(options, mediator)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTag> Tags { get; set; }
        public DbSet<ArticleClassify> ArticleClassifies { get; set; }
        public DbSet<ArticleContent> ArticleContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
