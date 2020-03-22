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

        public AddBookModel(IBookCopiesService bookCopiesService, ILibraryService libraryService)
        {
            this.bookCopiesService = bookCopiesService;
            this.libraryService = libraryService;
        }
        [BindProperty]
        public IEnumerable<SelectListItem> SelectBook { get; set; }
        public Library Library { get; set; }
        public IActionResult OnGet(int id)
        {
           

            return Page();
        }
        public IActionResult OnPost()
        {
            
            return Page();
        }
    }
}