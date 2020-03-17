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
    public class AuthorDetailsModel : PageModel
    {
        private readonly IAuthorService authorService;

        public AuthorDetailsModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }
        public Author Author { get; set; }
        [TempData]
        public string Message { get; set; }
        public IActionResult OnGet(int id)
        {
            Author = authorService.GetAuthorById(id);
            if(Author == null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();
        }
    }
}