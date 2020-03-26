using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.BookCopies
{
    public class BookCopiesViewModel
    {
        public IEnumerable<LibraryProject.Domain.BookCopies> BookCopies { get; set; }
    }
}
