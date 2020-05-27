using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class TitleAuthorData : ITitleAuthorService
    {
        private readonly AppDbContext dbContext;

        public TitleAuthorData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public TitleAuthor GetAuthorForTitleAuthor(int id)
        {
            return dbContext.TitleAuthors.SingleOrDefault(a => a.AuthorId == id);
        }
    }
}
