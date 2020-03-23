using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Books
{
    public class BooksIndexViewModel
    {
        public string SearchBook { get; set; }
        public string SearchAuthor { get; set; }

        public string Message { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
