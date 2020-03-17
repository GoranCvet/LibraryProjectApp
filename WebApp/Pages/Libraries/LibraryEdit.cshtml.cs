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
    public class LibraryEditModel : PageModel
    {
        private readonly ILibraryService libraryService;

        public LibraryEditModel(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }
        [BindProperty]
        public Library Library { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Library = libraryService.GetLibraryById(id.Value);
                if(Library == null)
                {
                    return RedirectToPage("NotFound");
                }
            }
            else
            {
                Library = new Library();
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Library.Id == 0)
                {
                    Library = libraryService.CreateLibrary(Library);
                }
                else
                {
                    Library = libraryService.UpdateLibrary(Library);
                }

                libraryService.Commit();

                return RedirectToPage("LibrariesList", new { id = Library.Id});
            }

            return Page();
        }
    }
}