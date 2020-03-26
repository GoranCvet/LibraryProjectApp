using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Libraries
{
    public class LibraryDetailsViewModel
    {
        public string Message { get; set; }
        public Library Library { get; set; }
    }
}
