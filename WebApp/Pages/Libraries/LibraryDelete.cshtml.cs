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
    public class LibraryDeleteModel : PageModel
    {
        private readonly ILibraryService libraryService;

        public LibraryDeleteModel(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }
        [BindProperty]
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
        public IActionResult OnPost()
        {
            var temp = libraryService.DeleteLibrary(Library.Id);
            if (temp == null)
            {
                return RedirectToPage("NotFound");
            }

            libraryService.Commit();
            TempData["Message"] = "Library Deleted";

            return RedirectToPage("LibrariesList");
        }
    }
}