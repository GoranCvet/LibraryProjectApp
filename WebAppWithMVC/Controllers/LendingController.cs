using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppWithMVC.Models.Lendings;

namespace WebAppWithMVC.Controllers
{
    public class LendingController : Controller
    {
        private readonly ILendingService lendingService;
        private readonly IClientService clientService;
        private readonly ILibraryService libraryService;
        private readonly IBookService bookService;

        public LendingController(ILendingService lendingService, IClientService clientService,ILibraryService libraryService,IBookService bookService)
        {
            this.lendingService = lendingService;
            this.clientService = clientService;
            this.libraryService = libraryService;
            this.bookService = bookService;
        }
        public IActionResult Index()
        {
            var model = new LendingIndexViewModel();
            model.Lendings = lendingService.GetLendings();
            return View(model);
        }
        public IActionResult CreateLending(int? id)
        {
            var model = new LendingCreateViewModel();

            model.Client = clientService.GetClientById(id.Value);
            model.SelectClient = clientService.GetClients().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            model.SelectBook = bookService.GetBooks().Select(b => new SelectListItem
            {
                Text = b.Title,
                Value = b.Id.ToString()
            });
            model.SelectLibrary = libraryService.GetLibraries().Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });


            var lending = new Lending();
            lending.ClientId = id.Value;
            model.Lending = lending;

            return View(model);

        }
        [HttpPost]
        public IActionResult CreateLending(LendingCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Lending = lendingService.CreateLending(model.Lending);
                if (model.Lending == null)
                {
                    TempData["Message"] = "There are not book copies available!";
                }

                lendingService.Commit();

                return RedirectToAction("Details", "Client", new { id = model.Lending.ClientId });
            }

            model.SelectClient = clientService.GetClients().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            model.SelectBook = bookService.GetBooks().Select(b => new SelectListItem
            {
                Text = b.Title,
                Value = b.Id.ToString()
            });
            model.SelectLibrary = libraryService.GetLibraries().Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });

            return View(model);
        }
        public IActionResult ReturnLending(int id)
        {
            var lending = lendingService.GetLendingById(id);
            if(lending == null)
            {
                return View("NotFound");
            }
            var library = libraryService.GetLibraryById(lending.LibraryId);
            lending.Library = library;
            return View(lending);
        }
        [HttpPost]
        public IActionResult ReturnLending(Lending model)
        {
            if (ModelState.IsValid)
            {
                model = lendingService.ReturnLentBook(model);
                lendingService.Commit();
                TempData["Message"] = "Book returned!";

                return RedirectToAction("Details","Client", new { id = model.ClientId });
            }

            return View(model);
        }
        

    }
}