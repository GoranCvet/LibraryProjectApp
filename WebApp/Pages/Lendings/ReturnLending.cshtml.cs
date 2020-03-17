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
    public class ReturnLendingModel : PageModel
    {
        private readonly ILendingService lendingService;
        private readonly IClientService clientService;

        public ReturnLendingModel(ILendingService lendingService, IClientService clientService)
        {
            this.lendingService = lendingService;
            this.clientService = clientService;
        }
        [BindProperty]
        public Lending Lending { get; set; }
        public IActionResult OnGet(int id)
        {
            Lending = lendingService.GetLendingById(id);
            if(Lending == null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                Lending = lendingService.ReturnLentBook(Lending);
                lendingService.Commit();
                TempData["Message"] = "Book returned!";

                return RedirectToPage("/Clients/ClientList");
            }

            return Page();
        }
    }
}