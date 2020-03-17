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
    public class LibrariesListModel : PageModel
    {
        private readonly ILibraryService libraryService;

        public LibrariesListModel(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }
        public IEnumerable<Library> Libraries { get; set; }
        public void OnGet()
        {
            Libraries = libraryService.GetLibraries();
        }
    }
}