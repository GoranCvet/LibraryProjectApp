using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Lendings
{
    public class LendingIndexViewModel
    {
        public string Message { get; set; }
        public IEnumerable<Lending> Lendings { get; set; }
    }
}
