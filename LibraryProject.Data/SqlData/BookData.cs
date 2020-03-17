using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class BookData : IBookService
    {
        private readonly AppDbContext dbContext;

        public BookData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Book CreateBook(Book book)
        {
            var temp = dbContext.Books.Add(book);
            return temp.Entity;
        }

        public Book Delete(int id)
        {
            var temp = dbContext.Books.SingleOrDefault(b => b.Id == id);
            if(temp != null)
            {
                dbContext.Books.Remove(temp);
            }
            return temp;
        }

        public Book GetBookById(int id)
        {
            return dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCopies)
                .SingleOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetBooks(string book = null, string author = null)
        {
            var titlePattern = !string.IsNullOrEmpty(book) ? $"{book}%" : book;
            var authorPattern = !string.IsNullOrEmpty(author) ? $"{author}%" : author;

            return dbContext.Books
                .Include(b => b.Author)
                .Where(b => string.IsNullOrEmpty(book) || EF.Functions.Like(b.Title, titlePattern))
                .Where(a => string.IsNullOrEmpty(author) || EF.Functions.Like(a.Author.Name, authorPattern))
                .OrderBy(b => b.Title)
                .ToList();
        }

        public Book UpdateBook(Book book)
        {
            dbContext.Books.Update(book);
            return book;
        }
    }
}
