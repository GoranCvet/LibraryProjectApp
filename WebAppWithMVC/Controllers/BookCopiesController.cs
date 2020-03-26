using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppWithMVC.Models.BookCopies;

namespace WebAppWithMVC.Controllers
{
    public class BookCopiesController : Controller
    {
        private readonly IBookCopiesService bookCopiesService;
        private readonly IBookService bookService;
        private readonly ILibraryService libraryService;

        public BookCopiesController(IBookCopiesService bookCopiesService, IBookService bookService, ILibraryService libraryService)
        {
            this.bookCopiesService = bookCopiesService;
            this.bookService = bookService;
            this.libraryService = libraryService;
        }
        public IActionResult Index()
        {
            var model = new BookCopiesViewModel();
            model.BookCopies = bookCopiesService.GetBookCopies();
            return View(model);
        }

        public IActionResult CreateCopy(int id)
        {
            var model = new CreateCopyViewModel();
            model.Book = bookService.GetBookById(id);
            model.SelectLibrary = libraryService.GetLibraries().Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });

            return View(model);

        }
        [HttpPost]
        public IActionResult CreateCopy(CreateCopyViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.BookCopy.BookId = model.Book.Id;
                var Library = libraryService.GetLibraryById(model.BookCopy.LibraryId);
                model.BookCopy = bookCopiesService.CreateBookCopy(model.BookCopy);

                Library.BookCopies.Add(model.BookCopy);

                bookCopiesService.Commit();
                return RedirectToAction("Details","Library",new { id = Library.Id });
            }

            model.SelectLibrary = libraryService.GetLibraries().Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });
            return View(model);
        }
        public IActionResult EditCopyInLibrary(int bookId, int libraryId)
        {
            var copy = new BookCopies();
            copy = bookCopiesService.GetBookCopyById(bookId, libraryId);
            if (copy == null)
            {
                return View("NotFound");
            }

            return View(copy);
        }
        [HttpPost]
        public IActionResult EditCopyInLibrary(BookCopies copy)
        {
            if (ModelState.IsValid)
            {
                copy = bookCopiesService.UpdateCopy(copy);

                bookCopiesService.Commit();
                TempData["Message"] ="Changes in copies applied!";

                return RedirectToAction("Details", "Library", new { id = copy.LibraryId });
            }

            return View(copy);
        }
    }
}
