using LibraryProject.Domain;
using LibraryProject.Domain.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<TitleAuthor> TitleAuthors { get; set; }
        public DbSet<BookCopies> BookCopies { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Lending> Lendings { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCopies>().HasKey(bc => new { bc.BookId, bc.LibraryId });
            modelBuilder.Entity<TitleAuthor>().HasKey(ta => new { ta.AuthorId, ta.BookId });
            base.OnModelCreating(modelBuilder);
        }



    }
}
