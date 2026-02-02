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

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users");
                u.Property(u => u.Nom).IsRequired();
                u.Property(u=>u.Token).IsRequired();
                u.HasMany(u=>u.Taches)
                .WithOne(t=>t.User)
                .HasForeignKey(t=>t.User.Id);
            });

            modelBuilder.Entity<Tache>(t =>
            {
                t.ToTable("Taches"); 
                t.Property(t=>t.Titre).IsRequired();
                t.Property(t=>t.DateDebut)
                .IsRequired();
                t.Property(t => t.DateFin)
                .IsRequired(false);

                t.HasOne(t=>t.User)
                .WithMany(u=>u.Taches)
                .HasForeignKey(t=>t.User.Id);


            });
            //    .HasMany(u=>u.Taches)
            //    .WithOne(t=>t.User)
            //    .HasForeignKey(t=>t.UserId)
            //    .IsRequired();

            //var entitybuilder= modelBuilder.Entity<Tache>();
            //entitybuilder.Property(t => t.Titre)
            //      .IsRequired()
            //      .HasMaxLength(100);
            //entitybuilder.Property(t => t.DateDebut)
            //    .IsRequired();


            //base.OnModelCreating(modelBuilder);
        }


    }
}
