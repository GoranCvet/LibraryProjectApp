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
        private readonly IBookCopiesService bookCopiesService;

        public LibraryDetailsModel(ILibraryService libraryService, IBookCopiesService bookCopiesService)
        {
            this.libraryService = libraryService;
            this.bookCopiesService = bookCopiesService;
        }
        [TempData]
        public string Message { get; set; }
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
        public IActionResult OnPostDelete(int bookId, int libraryId)
        {
            var temp = bookCopiesService.DeleteCopy(bookId, libraryId);
            if(temp == null)
            {
                return RedirectToPage("NotFound");
            }
            bookCopiesService.Commit();
            TempData["Message"] = "Book Copy Removed!";

            return RedirectToPage("/Libraries/LibraryDetails", new { id = temp.LibraryId });
        }

    }
}