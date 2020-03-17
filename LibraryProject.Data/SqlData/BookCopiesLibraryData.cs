using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class BookCopiesLibraryData : IBookCopiesLibraryService
    {
        private readonly AppDbContext dbContext;

        public BookCopiesLibraryData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public BookCopiesLibrary Create(BookCopiesLibrary bookCopiesLibrary)
        {
            var bookCopy = dbContext.BookCopies.SingleOrDefault(bc => bc.Id == bookCopiesLibrary.BookCopiesId);
            if(bookCopy != null && bookCopiesLibrary.LibriryCopies <= bookCopy.NumberOfCopies)
            {
                dbContext.BookCopiesLibraries.Add(bookCopiesLibrary);
                bookCopy.NumberOfCopies -= bookCopiesLibrary.LibriryCopies;
            }
            return bookCopiesLibrary;
        }

        public IEnumerable<BookCopiesLibrary> Get()
        {
            return dbContext.BookCopiesLibraries
                .Include(bl => bl.BookCopies)
                .ThenInclude(bc => bc.Book)
                .ToList();
        }
    }
}
