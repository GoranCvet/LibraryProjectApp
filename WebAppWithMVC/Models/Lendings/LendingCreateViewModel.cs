using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Lendings
{
    public class LendingCreateViewModel
    {
        public Lending Lending { get; set; }
        public IEnumerable<SelectListItem> SelectClient { get; set; }
        public IEnumerable<SelectListItem> SelectLibrary { get; set; }
        public IEnumerable<SelectListItem> SelectBook { get; set; }
        public Client Client { get; set; }


    }
}
