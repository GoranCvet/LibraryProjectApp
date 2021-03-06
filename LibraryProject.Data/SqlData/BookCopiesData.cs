﻿using LibraryProject.Data.IServices;
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
       

        public IEnumerable<BookCopies> GetBookCopies()
        {
            return dbContext.BookCopies
                .Include(bc => bc.Book)
                .Include(bc => bc.Library)
                .ToList();
        }

        public BookCopies CreateBookCopy(BookCopies bookCopy)
        {
            var temp = dbContext.BookCopies.Add(bookCopy);
            return temp.Entity;
        }

        public BookCopies GetBookCopyById(int bookId, int libraryId)
        {
            return dbContext.BookCopies.SingleOrDefault(bc => bc.BookId == bookId && bc.LibraryId == libraryId);
        }

        public BookCopies DeleteCopy(int bookId, int libraryId)
        {
            var temp = dbContext.BookCopies.SingleOrDefault(bc => bc.BookId == bookId && bc.LibraryId == libraryId);
            if (temp != null)
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
