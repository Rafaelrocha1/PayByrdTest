using Microsoft.EntityFrameworkCore;
using paybyrd.Entities;

namespace paybyrd.Context
{
    public class PayByrdContext : DbContext
    {
        public PayByrdContext(DbContextOptions<PayByrdContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diff>(entity =>
            {                

                    entity.HasKey(x => new {x.Id, x.TypeDiff });               
            });
        }
        public DbSet<Diff> Diff { get; set; }
        
    }
}