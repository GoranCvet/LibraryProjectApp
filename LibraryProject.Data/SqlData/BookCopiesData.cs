using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class BookCopiesData : IBookCopiesService
    {
        private readonly AppDbContext dbContext;

        public BookCopiesData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        

        public int Commit()
        {
            return dbContext.SaveChanges();
        }
        public BookCopies GetBookCopyById(int id)
        {
            return dbContext.BookCopies.Include(bc => bc.Book)
                 .SingleOrDefault(bc => bc.Id == id);
        }

        public IEnumerable<BookCopies> GetBookCopies()
        {
            return dbContext.BookCopies
                .Include(bc => bc.Book)
                .ToList();
        }

        public BookCopies CreateBookCopy(BookCopies bookCopy)
        {
            var temp = dbContext.BookCopies.Add(bookCopy);
            return temp.Entity;
        }

        public BookCopies DeleteCopy(int id)
        {
            var temp = dbContext.BookCopies.SingleOrDefault(bc => bc.Id == id);
            if(temp != null)
            {
                dbContext.BookCopies.Remove(temp);
            }

            return temp;
        }

        public BookCopies UpdateCopy(BookCopies bookCopy)
        {
            dbContext.Entry(bookCopy).State = EntityState.Modified;
            return bookCopy;
        }
    }
}
