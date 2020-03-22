using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class LendingData : ILendingService
    {
        private readonly AppDbContext dbContext;

        public LendingData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Lending CreateLending(Lending lending)
        {

            var bookCopy = dbContext.BookCopies.SingleOrDefault(bc => bc.BookId == lending.BookId && bc.LibraryId == lending.LibraryId);

            if (bookCopy != null && bookCopy.NumberOfCopies != 0)
            {
                dbContext.Lendings.Add(lending);
                bookCopy.NumberOfCopies -= 1;
                return lending;
            }
            else
            {
                return lending = null;
            }
        }

        public Lending GetLendingById(int id)
        {
            return dbContext.Lendings
                .Include(l => l.Book)
                .SingleOrDefault(l => l.Id == id);
        }

        public IEnumerable<Lending> GetLendings()
        {
            return dbContext.Lendings
                .Include(l => l.Client)
                .Include(l => l.Book)
                .OrderBy(l => l.Id)
                .ToList();
        }

        public Lending ReturnLentBook(Lending lending)
        {

            var bookCopy = dbContext.BookCopies.SingleOrDefault(bc => bc.BookId == lending.BookId && bc.LibraryId == lending.LibraryId);
            bookCopy.NumberOfCopies += 1;
            dbContext.Entry(lending).State = EntityState.Modified;

            return lending;
        }
    }
}
