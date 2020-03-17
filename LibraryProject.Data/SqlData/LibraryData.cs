using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class LibraryData : ILibraryService
    {
        private readonly AppDbContext dbContext;

        public LibraryData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Library CreateLibrary(Library library)
        {
            var temp = dbContext.Libraries.Add(library);
            return temp.Entity;
        }

        public Library DeleteLibrary(int id)
        {
            var temp = dbContext.Libraries.SingleOrDefault(l => l.Id == id);
            if(temp != null)
            {
                dbContext.Libraries.Remove(temp);
            }
            return temp;
        }

        public IEnumerable<Library> GetLibraries()
        {
            return dbContext.Libraries.OrderBy(l => l.Name).ToList();
                
        }

        public Library GetLibraryById(int id)
        {
            return dbContext.Libraries
                .Include(lb => lb.BookCopiesLibraries)
                .ThenInclude(bl => bl.BookCopies)
                .ThenInclude(bc => bc.Book)
                .OrderBy(lb => lb.Name)
                .SingleOrDefault(lb => lb.Id == id);

        }

        public Library UpdateLibrary(Library library)
        {
            dbContext.Entry(library).State = EntityState.Modified;
            return library;
        }
    }
}
