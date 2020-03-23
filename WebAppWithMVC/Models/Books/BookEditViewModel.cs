using LibraryProject.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Books
{
    public class BookEditViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> SelectAuthor { get; set; }
        public IEnumerable<SelectListItem> SelectPublisher { get; set; }
        public IFormFile Photo { get; set; }


    }
}
