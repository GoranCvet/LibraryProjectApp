using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Authors
{
    public class AuthorsIndexViewModel
    {
        public string SearchAuthor { get; set; }
        public string Message { get; set; }
        public IEnumerable<Author> Authors { get; set; }

    }
}
