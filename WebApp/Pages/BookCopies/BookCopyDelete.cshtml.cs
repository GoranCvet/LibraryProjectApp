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
    public class BookCopyDeleteModel : PageModel
    {
        private readonly IBookCopiesService bookCopiesService;

        public BookCopyDeleteModel(IBookCopiesService bookCopiesService)
        {
            this.bookCopiesService = bookCopiesService;
        }
        [BindProperty]
        public BookCopies BookCopy { get; set; }
        public IActionResult OnGet(int id)
        {
            //BookCopy = bookCopiesService.GetBookCopyById(id);
            //if(BookCopy == null)
            //{
            //    return RedirectToPage("NotFound");
            //}
            return Page();
        }

        public IActionResult OnPost()
        {
            //var temp = bookCopiesService.DeleteCopy(BookCopy.Id);
            //if(temp == null)
            //{
            //    return RedirectToPage("NotFound");
            //}

            bookCopiesService.Commit();

            return RedirectToPage("BookCopiesList");
        }
    }
}