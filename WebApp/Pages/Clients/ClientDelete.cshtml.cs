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
    public class ClientDeleteModel : PageModel
    {
        private readonly IClientService clientService;

        public ClientDeleteModel(IClientService clientService)
        {
            this.clientService = clientService;
        }
        [BindProperty]
        public Client Client { get; set; }
        public IActionResult OnGet(int id)
        {
            Client = clientService.GetClientById(id);
            if(Client == null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();

        }
        public IActionResult OnPost()
        {
            var temp = clientService.DeleteClient(Client.Id);
            if(temp == null)
            {
                return RedirectToPage("NotFound");
            }

            clientService.Commit();
            TempData["Message"] = "Client Deleted!";
            return RedirectToPage("ClientList");
        }
    }
}