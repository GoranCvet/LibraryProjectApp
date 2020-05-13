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
        private readonly IBookCopiesService bookCopiesService;
        private readonly ILibraryService libraryService;
        private readonly IBookService bookService;

        public AddBookModel(IBookCopiesService bookCopiesService, ILibraryService libraryService,IBookService bookService)
        {
            this.bookCopiesService = bookCopiesService;
            this.libraryService = libraryService;
            this.bookService = bookService;
        }
        public IEnumerable<SelectListItem> SelectBook { get; set; }
        [BindProperty]
        public Library Library { get; set; }
        [BindProperty]
        public BookCopies BookCopy { get; set; }
        public IActionResult OnGet(int id)
        {
            Library = libraryService.GetLibraryById(id);
            if(Library == null)
            {
                return RedirectToPage("NotFound");
            }

            SelectBook = FilterBooks();
            BookCopy = new BookCopies();

            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                BookCopy.LibraryId = Library.Id;
                BookCopy = bookCopiesService.CreateBookCopy(BookCopy);
                TempData["Message"] = "Book Copies Added to Library";

                Library.BookCopies.Add(BookCopy);
                bookCopiesService.Commit();
                return RedirectToPage("/Libraries/LibraryDetails", new { id = BookCopy.LibraryId });
            }



            SelectBook = bookService.GetBooks().Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Title
            });
            return Page();
        }

        private IEnumerable<SelectListItem> FilterBooks()
        {
            var list = bookService.GetBooks();
            var books = new List<Book>();
            foreach(var item in list)
            {
                if(!item.BookCopies.Any(bc => bc.LibraryId == Library.Id))
                {
                    books.Add(item);
                }
            }
            var selectListItem = books.Select(b => new SelectListItem
            {
                Text = b.Title,
                Value = b.Id.ToString()
            });

            return selectListItem;
        }
    }
}