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
    public class ClientDetailsModel : PageModel
    {
        private readonly IClientService clientService;
        private readonly ILendingService lendingService;

        public ClientDetailsModel(IClientService clientService, ILendingService lendingService)
        {
            this.clientService = clientService;
            this.lendingService = lendingService;
        }
        public Client Client { get; set; }
        public IEnumerable<Lending> Lendings { get; set; }
        [TempData]
        public string Message { get; set; }
        public IActionResult OnGet(int id)
        {
            Client = clientService.GetClientById(id);
            if(Client == null)
            {
                return RedirectToPage("NotFound");
            }
            Lendings = lendingService.GetLendings();
            return Page();
        }
    }
}