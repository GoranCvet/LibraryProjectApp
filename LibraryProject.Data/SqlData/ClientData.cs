using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.Data.SqlData
{
    public class ClientData : IClientService
    {
        private readonly AppDbContext dbContext;

        public ClientData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Client CreateClient(Client client)
        {
            var temp = dbContext.Clients.Add(client);
            return temp.Entity;
        }

        public Client DeleteClient(int id)
        {
            var temp = dbContext.Clients.SingleOrDefault(c => c.Id == id);
            if(temp != null)
            {
                dbContext.Clients.Remove(temp);
            }
            return temp;
        }

        public Client GetClientById(int id)
        {
            return dbContext.Clients
                .Include(c => c.Lendings)
                .ThenInclude(l => l.Library)
                .ThenInclude(l => l.BookCopies) // Ova ->
                .ThenInclude(bc => bc.Book) // I ova mi treba za da mozam da gi prikazam podatocite vo MVC
                .SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Client> GetClients(string input = null)
        {
            return dbContext.Clients
                .Where(c => string.IsNullOrEmpty(input) || c.Name.Contains(input))
                .OrderBy(c => c.Name)
                .ToList();
                
        }

        public Client UpdateClient(Client client)
        {
            dbContext.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return client;
        }
    }
}
