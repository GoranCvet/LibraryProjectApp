using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Clients
{
    public class ClientIndexViewModel
    {
        public string SearchTerm { get; set; }
        public string Message { get; set; }

        public IEnumerable<Client> Clients { get; set; }
    }
}
