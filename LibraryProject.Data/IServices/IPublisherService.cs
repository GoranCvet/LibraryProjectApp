using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface IPublisherService
    {
        IEnumerable<Publisher> GetPublishers();
        Publisher GetPublisherById(int id);
        int Commit();
        Publisher CreatePublisher(Publisher publisher);
        Publisher UpdatePublisher(Publisher publisher);
        Publisher DeletePublisher(int id);
    }
}
