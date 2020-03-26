using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Libraries
{
    public class LibraryIndexViewModel
    {
        public IEnumerable<Library> Libraries { get; set; }
        public string Message { get; set; }

    }
}
