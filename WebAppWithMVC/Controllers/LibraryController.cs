using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using WebAppWithMVC.Models.Libraries;

namespace WebAppWithMVC.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService libraryService;
        private readonly IBookCopiesService bookCopiesService;

        public LibraryController(ILibraryService libraryService, IBookCopiesService bookCopiesService)
        {
            this.libraryService = libraryService;
            this.bookCopiesService = bookCopiesService;
        }
        public IActionResult Index()
        {
            var model = new LibraryIndexViewModel();
            model.Libraries = libraryService.GetLibraries();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var model = new LibraryDetailsViewModel();
            model.Library = libraryService.GetLibraryById(id);
            if(model.Library == null)
            {
                return View("NotFound");
            }
            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            var library = new Library();
            if (id.HasValue)
            {
                library = libraryService.GetLibraryById(id.Value);
                if(library == null)
                {
                    return View("NotFound");
                }
            }
            else
            {
                library = new Library();
            }
            return View(library);
        }
        [HttpPost]
        public IActionResult Edit(Library library)
        {
            if (ModelState.IsValid)
            {
                if(library.Id == 0)
                {
                    library = libraryService.CreateLibrary(library);
                    TempData["Message"] = "Library Created!";
                }
                else
                {
                    library = libraryService.UpdateLibrary(library);
                    TempData["Message"] = "Library Editted!";

                }
                libraryService.Commit();

                return RedirectToAction("Details", new { id = library.Id });
            }

            return View(library);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var temp = libraryService.DeleteLibrary(id);
            if(temp == null)
            {
                return View("NotFound");
            }
            libraryService.Commit();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteBookCopy(int bookId, int libraryId)
        {
            var copy = bookCopiesService.DeleteCopy(bookId, libraryId);

            bookCopiesService.Commit();

            return RedirectToAction("Details", new { id = copy.LibraryId });
        }
    }
}