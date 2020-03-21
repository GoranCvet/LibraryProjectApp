using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp
{
    public class AddBookModel : PageModel
    {
        private readonly IBookCopiesLibraryService bookCopiesLibraryService;
        private readonly IBookCopiesService bookCopiesService;
        private readonly ILibraryService libraryService;

        public AddBookModel(IBookCopiesLibraryService bookCopiesLibraryService, IBookCopiesService bookCopiesService
            , ILibraryService libraryService)
        {
            this.bookCopiesLibraryService = bookCopiesLibraryService;
            this.bookCopiesService = bookCopiesService;
            this.libraryService = libraryService;
        }
        [BindProperty]
        public BookCopiesLibrary BookCopiesLibrary { get; set; }
        public IEnumerable<SelectListItem> SelectBook { get; set; }
        public Library Library { get; set; }
        public IActionResult OnGet(int id)
        {
            Library = libraryService.GetLibraryById(id);
            if(Library == null)
            {
                return RedirectToPage("NotFound");
            }
            BookCopiesLibrary = new BookCopiesLibrary();
            BookCopiesLibrary.LibraryId = Library.Id;
            SelectBook = bookCopiesService.GetBookCopies().Select(b => new SelectListItem
            {
                Text = b.Book.Title,
                Value = b.Id.ToString()
            }); ;

            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                BookCopiesLibrary = bookCopiesLibraryService.Create(BookCopiesLibrary);

                bookCopiesLibraryService.Commit();
                Library = libraryService.GetLibraryById(BookCopiesLibrary.LibraryId);
                return RedirectToPage("/Libraries/LibraryDetails", new { id = Library.Id });
            }

            return Page();
        }
    }
}