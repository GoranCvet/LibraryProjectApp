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
    public class BookCopiesListModel : PageModel
    {
        private readonly IBookCopiesService bookCopiesService;

        public BookCopiesListModel(IBookCopiesService bookCopiesService)
        {
            this.bookCopiesService = bookCopiesService;
        }
        public IEnumerable<BookCopies> BookCopies { get; set; }

        [TempData]
        public string Message { get; set; }
        public void OnGet()
        {
            BookCopies = bookCopiesService.GetBookCopies();
        }
    }
}