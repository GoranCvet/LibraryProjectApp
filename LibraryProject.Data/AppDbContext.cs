using LibraryProject.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopies> BookCopies { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Lending> Lendings { get; set; }
        public DbSet<BookCopiesLibrary> BookCopiesLibraries { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCopiesLibrary>().HasKey(bl => new { bl.BookCopiesId, bl.LibraryId });
            base.OnModelCreating(modelBuilder);
        }

       

    }
}
