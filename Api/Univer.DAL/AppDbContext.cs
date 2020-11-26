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


        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(options: dbContextOptions) { }

        
    }
}
