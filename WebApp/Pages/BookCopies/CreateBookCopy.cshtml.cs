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
    public class CreateBookCopyModel : PageModel
    {
        private readonly IBookCopiesService bookCopiesService;
        private readonly IBookService bookService;
        private readonly ILibraryService libraryService;

        public CreateBookCopyModel(IBookCopiesService bookCopiesService, IBookService bookService, ILibraryService libraryService)
        {
            this.bookCopiesService = bookCopiesService;
            this.bookService = bookService;
            this.libraryService = libraryService;
        }
        [BindProperty]
        public BookCopies BookCopy { get; set; }
        [BindProperty]
        public Book Book { get; set; }
        public Library Library { get; set; }
        public IEnumerable<SelectListItem> SelectLibrary { get; set; }
        public IActionResult OnGet(int id)
        {
            Book = bookService.GetBookById(id);
            if (Book == null)
            {
                return RedirectToPage("NotFound");
            }

            BookCopy = new BookCopies();
            
            SelectLibrary = FilterList();

            return Page();
            
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                BookCopy.BookId = Book.Id;
                Library = libraryService.GetLibraryById(BookCopy.LibraryId);
                BookCopy = bookCopiesService.CreateBookCopy(BookCopy);


                Library.BookCopies.Add(BookCopy);
                bookCopiesService.Commit();
            
                return RedirectToPage("BookCopiesList");
            }

            SelectLibrary = FilterList();
            return Page();
        }
        private IEnumerable<SelectListItem> FilterList()
        {
            var list = libraryService.GetLibraries();
            var library = new List<Library>();
            foreach (var item in list)
            {
                if (!item.BookCopies.Any(bc => bc.BookId == Book.Id))
                {
                    library.Add(item);
                }
            }
            var selectListItem = library.Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });
            return selectListItem;
        }
    }
}