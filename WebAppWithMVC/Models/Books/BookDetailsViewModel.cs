using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Books
{
    public class BookDetailsViewModel
    {
        public string Message { get; set; }
        public Book Book { get; set; }
    }
}
