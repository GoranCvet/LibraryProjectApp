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
        private readonly IBookCopiesLibraryService bookCopiesLibraryService;

        public LibraryDetailsModel(ILibraryService libraryService, IBookCopiesService bookCopiesService
            ,IBookCopiesLibraryService bookCopiesLibraryService)
        {
            this.libraryService = libraryService;
            this.bookCopiesService = bookCopiesService;
            this.bookCopiesLibraryService = bookCopiesLibraryService;
        }
        public Library Library { get; set; }
        public IEnumerable<BookCopiesLibrary> BookCopiesLibraries { get; set; }
        public IActionResult OnGet(int id)
        {
            Library = libraryService.GetLibraryById(id);
            if(Library == null)
            {
                return RedirectToPage("NotFound");
            }
            BookCopiesLibraries = bookCopiesLibraryService.Get();
            return Page();
        }

    }
}