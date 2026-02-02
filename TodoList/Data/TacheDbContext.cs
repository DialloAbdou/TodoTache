using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data
{
    public class TacheDbContext : DbContext
    {
        public DbSet<Tache> Taches => Set<Tache>();
        public DbSet<User> Users => Set<User>();

        public TacheDbContext(DbContextOptions<TacheDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // ======== USer =============
            modelBuilder.Entity<User>(entity=>
            {
                entity.ToTable("Users");
                entity.HasKey(t => t.Id);

                entity.Property(u => u.Nom)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(u => u.Token)
                .IsRequired()
                .HasMaxLength(10);

                entity.HasMany(u => u.Taches)
                .WithOne(t => t.User)
                .HasForeignKey(t=>t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            // ============Tache =========================
            modelBuilder.Entity<Tache>(entity =>
            {
                entity.ToTable("Taches");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Titre)
                .IsRequired()
                .HasMaxLength(150);

                entity.Property(t => t.DateDebut)
                .IsRequired();

                entity.Property(t => t.DateFin)
                .IsRequired(false);

                entity.HasIndex(t => t.UserId);


            });

   

        }


    }
}
