using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class AuthorData : IAuthorService
    {
        private readonly AppDbContext dbContext;

        public AuthorData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Author CreateAuthor(Author author)
        {
            var temp = dbContext.Authors.Add(author);
            return temp.Entity;
        }

        public Author DeleteAuthor(int id)
        {
            var temp = dbContext.Authors.SingleOrDefault(a => a.Id == id);
            if(temp != null)
            {
                dbContext.Authors.Remove(temp);
            }
            return temp;
        }

        public Author GetAuthorById(int id)
        {
            return dbContext.Authors.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Author> GetAuthors(string input = null)
        {
            return dbContext.Authors
                .Where(a => string.IsNullOrEmpty(input) || a.Name.Contains(input))
                .OrderBy(a => a.Name)
                .ToList();
        }

        public Author UpdateAuthor(Author author)
        {
            dbContext.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return author;
        }
    }
}
