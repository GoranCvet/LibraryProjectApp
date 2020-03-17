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
    public class LendingListModel : PageModel
    {
        private readonly ILendingService lendingService;

        public LendingListModel(ILendingService lendingService)
        {
            this.lendingService = lendingService;
        }
        [TempData]
        public string Message { get; set; }
        public IEnumerable<Lending> Lendings { get; set; }
        public void OnGet()
        {
            Lendings = lendingService.GetLendings();
        }
    }
}