using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data
{
    public class TacheDbContext : DbContext
    {
        public DbSet<Tache> Taches => Set<Tache>();

        public TacheDbContext(DbContextOptions<TacheDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entitybuilder = modelBuilder.Entity<Tache>();

            entitybuilder.Property(t => t.Titre)
                  .IsRequired()
                  .HasMaxLength(100);

            entitybuilder.Property(t => t.DateDebut)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }


    }
}
