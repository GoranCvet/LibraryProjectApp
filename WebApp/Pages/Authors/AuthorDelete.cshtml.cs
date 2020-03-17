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
    public class AuthorDeleteModel : PageModel
    {
        private readonly IAuthorService authorService;

        public AuthorDeleteModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }
        [BindProperty]
        public Author Author { get; set; }
        public IActionResult OnGet(int id)
        {
            Author = authorService.GetAuthorById(id);
            if(Author == null)
            {
                return RedirectToPage("NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var temp = authorService.DeleteAuthor(Author.Id);
            if(temp == null)
            {
                return RedirectToPage("NotFound");
            }

            authorService.Commit();
            TempData["Message"] = "Author Deleted!";

            return RedirectToPage("AuthorList");
        }
    }
}