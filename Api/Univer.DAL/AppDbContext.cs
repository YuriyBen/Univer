using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Univer.DAL.Entities;

namespace Univer.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserPublicData> UsersPublicData { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<History> History { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(options: dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>()
                .HasOne(g => g.UserPublicData)
                .WithMany(s => s.History);

            modelBuilder.Entity<History>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.Id);
            });
        }
    }
}
