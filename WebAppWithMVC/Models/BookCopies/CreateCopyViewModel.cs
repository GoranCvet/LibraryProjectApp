using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.BookCopies
{
    public class CreateCopyViewModel
    {
        public LibraryProject.Domain.BookCopies BookCopy { get; set; }
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> SelectLibrary { get; set; }
    }
}
