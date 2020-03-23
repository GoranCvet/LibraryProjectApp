using LibraryProject.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Authors
{
    public class AuthorEditViewModelcs
    {
        public Author Author { get; set; }
        public IFormFile Photo { get; set; }
    }
}
