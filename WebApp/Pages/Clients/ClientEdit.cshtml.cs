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
    public class ClientEditModel : PageModel
    {
        private readonly IClientService clientService;

        public ClientEditModel(IClientService clientService)
        {
            this.clientService = clientService;
        }
        [BindProperty]
        public Client Client { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Client = clientService.GetClientById(id.Value);
                if(Client == null)
                {
                    return RedirectToPage("NotFound");
                }
            }
            else
            {
                Client = new Client();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Client.Id == 0)
                {
                    Client = clientService.CreateClient(Client);
                    TempData["Message"] = "Client Created!";
                }
                else
                {
                    Client = clientService.UpdateClient(Client);
                    TempData["Message"] = "Client Updated!";
                }

                clientService.Commit();
                return RedirectToPage("ClientList");
            }

            return Page();
        }
    }
}