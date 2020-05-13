using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp
{
    public class CreateLendingModel : PageModel
    {
        private readonly ILendingService lendingService;
        private readonly IBookService bookService;
        private readonly IClientService clientService;
        private readonly ILibraryService libraryService;

        public CreateLendingModel(ILendingService lendingService, IBookService bookService, IClientService clientService,ILibraryService libraryService)
        {
            this.lendingService = lendingService;
            this.bookService = bookService;
            this.clientService = clientService;
            this.libraryService = libraryService;
        }
        [BindProperty]
        public DateTime DateTime { get; set; }
        [BindProperty]
        public Lending Lending { get; set; }
        public IEnumerable<SelectListItem> SelectBook { get; set; }
        public IEnumerable<SelectListItem> SelectClient { get; set; }
        public IEnumerable<SelectListItem> SelectLibrary { get; set; }
        [BindProperty]
        public Client Client { get; set; }
        public IActionResult OnGet(int? id)
        {
            Lending = new Lending();
            Lending.DatumZajmuvanje = DateTime.Now;

            if (id.HasValue)
            {
                Lending.ClientId = id.Value;
            }
            Client = clientService.GetClientById(id.Value);

            SelectBook = bookService.GetBooks().Select(b => new SelectListItem
            {
                Text = b.Title,
                Value = b.Id.ToString()
            });
            SelectClient = clientService.GetClients().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            SelectLibrary = libraryService.GetLibraries().Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });


            return Page();
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                Lending = lendingService.CreateLending(Lending);
                if(Lending == null)
                {
                    TempData["Message"] = "There are not book copies available!";
                    return RedirectToPage("/Lendings/LendingList");
                }

                lendingService.Commit();
                Client.Id = Lending.ClientId;

                return RedirectToPage("/Clients/ClientDetails", new { id = Client.Id});
            }

            SelectBook = bookService.GetBooks().Select(b => new SelectListItem
            {
                Text = b.Title,
                Value = b.Id.ToString()
            });
            SelectClient = clientService.GetClients().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            SelectLibrary = libraryService.GetLibraries().Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });


            return Page();

        }
    }
}