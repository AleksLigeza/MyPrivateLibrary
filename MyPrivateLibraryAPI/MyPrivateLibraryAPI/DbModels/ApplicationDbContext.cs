using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyPrivateLibraryAPI.DbModels
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserBook>()
                .HasKey(x => new { x.UserId, x.BookId });

            modelBuilder.Entity<Book>()
                .HasMany(x => x.UserBooks)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.UserBooks)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
