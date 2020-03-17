using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class PublisherData : IPublisherService
    {
        private readonly AppDbContext dbContext;

        public PublisherData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Publisher CreatePublisher(Publisher publisher)
        {
            var temp = dbContext.Publishers.Add(publisher);
            return temp.Entity;
        }

        public Publisher DeletePublisher(int id)
        {
            var temp = dbContext.Publishers.SingleOrDefault(p => p.Id == id);
            if(temp != null)
            {
                dbContext.Publishers.Remove(temp);
            }
            return temp;
        }

        public Publisher GetPublisherById(int id)
        {
            return dbContext.Publishers.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return dbContext.Publishers.OrderBy(p => p.Id).ToList();
        }

        public Publisher UpdatePublisher(Publisher publisher)
        {
            dbContext.Entry(publisher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return publisher;
        }
    }
}
