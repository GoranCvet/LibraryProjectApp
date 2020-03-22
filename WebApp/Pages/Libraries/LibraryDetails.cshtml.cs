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
    public class LibraryDetailsModel : PageModel
    {
        private readonly ILibraryService libraryService;

        public LibraryDetailsModel(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }
        public Library Library { get; set; }
        public IActionResult OnGet(int id)
        {
            Library = libraryService.GetLibraryById(id);
            if(Library == null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();
        }

    }
}