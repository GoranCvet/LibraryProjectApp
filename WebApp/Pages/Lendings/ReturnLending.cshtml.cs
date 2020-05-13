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
        private readonly ILibraryService libraryService;

        public ReturnLendingModel(ILendingService lendingService, ILibraryService libraryService)
        {
            this.lendingService = lendingService;
            this.libraryService = libraryService;
        }
        [BindProperty]
        public Lending Lending { get; set; }
        public Library Library { get; set; }
        public IActionResult OnGet(int id)
        {
            Lending = lendingService.GetLendingById(id);
            if(Lending == null)
            {
                return RedirectToPage("NotFound");
            }
            Library = libraryService.GetLibraryById(Lending.LibraryId);
            Lending.DatumVratena = DateTime.Now;
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Lending = lendingService.ReturnLentBook(Lending);
                lendingService.Commit();
                TempData["Message"] = "Book returned!";


                return RedirectToPage("/Clients/ClientDetails", new { id = Lending.ClientId });
            }

            return Page();
        }
    }
}