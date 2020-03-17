using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp
{
    public class ClientListModel : PageModel
    {
        private readonly IClientService clientService;

        public ClientListModel(IClientService clientService)
        {
            this.clientService = clientService;
        }
        public IEnumerable<Client> Clients { get; set; }
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }
        [TempData]
        public string Message { get; set; }
        public void OnGet()
        {
            Clients = clientService.GetClients(SearchTerm);
        }
    }
}