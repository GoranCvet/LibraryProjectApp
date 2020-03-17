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
    public class BookListModel : PageModel
    {
        private readonly IBookService bookService;

        public BookListModel(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public IEnumerable<Book> Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchBook { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchAuthor { get; set; }
        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            Books = bookService.GetBooks(SearchBook, SearchAuthor);
        }
    }
}