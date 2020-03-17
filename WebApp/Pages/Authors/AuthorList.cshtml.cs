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
    public class AuthorListModel : PageModel
    {
        private readonly IAuthorService authorService;

        public AuthorListModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }
        public IEnumerable<Author> Authors { get; set; }
        [TempData]
        public string Message { get; set; }
        [BindProperty(SupportsGet =true)]
        public string SearchAuthor { get; set; }
        public void OnGet()
        {
            Authors = authorService.GetAuthors(SearchAuthor);
        }
    }
}