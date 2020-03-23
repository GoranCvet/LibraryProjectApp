using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Authors
{
    public class AuthorDetailsViewModel
    {
        public string Message { get; set; }
        public Author Author { get; set; }
    }
}
