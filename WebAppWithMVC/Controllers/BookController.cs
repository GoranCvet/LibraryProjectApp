using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppWithMVC.Models.Books;

namespace WebAppWithMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService, IWebHostEnvironment webHostEnvironment)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.publisherService = publisherService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string SearchBook = null, string SearchAuthor = null)
        {
            var model = new BooksIndexViewModel();
            model.Books = bookService.GetBooks(SearchBook, SearchAuthor);
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var model = new BookDetailsViewModel();
            model.Book = bookService.GetBookById(id);
            if (model.Book == null)
            {
                return View("NotFound");
            }
            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            var model = new BookEditViewModel();
            if (id.HasValue)
            {
                model.Book = bookService.GetBookById(id.Value);
                if (model.Book == null)
                {
                    return View("NotFound");
                }
            }
            else
            {
                model.Book = new Book();
            }
            model.SelectAuthor = authorService.GetAuthors().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            model.SelectPublisher = publisherService.GetPublishers().Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(BookEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Book.Id == 0)
                {
                    if (model.Photo != null)
                    {
                        if (model.Book.BookPhoto != null)
                        {
                            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/bookImages/", model.Book.BookPhoto);
                            System.IO.File.Delete(filePath);
                        }
                        model.Book.BookPhoto = UploadBookPhoto(model);
                    }
                    model.Book = bookService.CreateBook(model.Book);

                    TempData["Message"] = "New Book Created!";
                }
                else
                {
                    if (model.Photo != null)
                    {
                        if (model.Book.BookPhoto != null)
                        {
                            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/bookImages/", model.Book.BookPhoto);
                            System.IO.File.Delete(filePath);
                        }
                        model.Book.BookPhoto = UploadBookPhoto(model);
                    }

                    model.Book = bookService.UpdateBook(model.Book);

                    TempData["Message"] = "Book updated!";
                }

                bookService.Commit();
                return RedirectToAction("Details", new { id = model.Book.Id });

            }
            model.SelectAuthor = authorService.GetAuthors().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            model.SelectPublisher = publisherService.GetPublishers().Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });
            return View(model);
        }
        private string UploadBookPhoto(BookEditViewModel model)
        {
            string uniqueName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/bookImages/");
                uniqueName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueName;
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = bookService.Delete(id);
            if(book == null)
            {
                return View("NotFound");
            }
            bookService.Commit();
            TempData["Message"] = "Book Removed!";

            return RedirectToAction("Index");

        }
    }
}