using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface IClientService
    {
        IEnumerable<Client> GetClients(string input = null);
        Client GetClientById(int id);
        int Commit();
        Client CreateClient(Client client);
        Client UpdateClient(Client client);
        Client DeleteClient(int id);
    }
}
