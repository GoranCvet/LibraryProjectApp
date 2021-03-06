﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp
{
    public class BookEditModel : PageModel
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IPublisherService publisherService;
        private readonly ITitleAuthorService titleAuthorService;

        public BookEditModel(IBookService bookService, IAuthorService authorService, IWebHostEnvironment webHostEnvironment,IPublisherService publisherService, ITitleAuthorService titleAuthorService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.webHostEnvironment = webHostEnvironment;
            this.publisherService = publisherService;
            this.titleAuthorService = titleAuthorService;
        }
        [BindProperty]
        public TitleAuthor TitleAuthor1 { get; set; }
        [BindProperty]
        public TitleAuthor TitleAuthor2 { get; set; }
        [BindProperty]
        public TitleAuthor TitleAuthor3 { get; set; }
        [BindProperty]
        public Book Book { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public IEnumerable<SelectListItem> SelectAuthor { get; set; }
        public IEnumerable<SelectListItem> SelectPublisher { get; set; }
        [BindProperty]
        public bool MultipleSelection { get; set; }


        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Book = bookService.GetBookById(id.Value);
                if (Book == null)
                {
                    return RedirectToPage("NotFound");
                }
            }
            else
            {
                Book = new Book();
            }

            SelectAuthor = authorService.GetAuthors().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });

            SelectPublisher = publisherService.GetPublishers().Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            return Page();
        }
        public IActionResult OnPost()
        {
            var author2 = new TitleAuthor();
            var author3 = new TitleAuthor();

            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    if (Photo != null)
                    {
                        if (Book.BookPhoto != null)
                        {
                            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/bookImages/", Book.BookPhoto);
                            System.IO.File.Delete(filePath);
                        }
                        Book.BookPhoto = UploadBookPhoto();
                    }

                    var list = new List<TitleAuthor>(){ TitleAuthor1};
                    if(TitleAuthor2.AuthorId != null)
                    {
                        list.Add(TitleAuthor2);
                    }
                    if(TitleAuthor3.AuthorId != null)
                    {
                        list.Add(TitleAuthor3);
                    }
                 
                    
                    foreach (var item in list)
                    {
                        if(item != null)
                        {
                            Book.TitleAuthors.Add(item);
                        }
                    }
                    Book = bookService.CreateBook(Book);
                    TempData["Message"] = "Book Created!";
                }
                else
                {
                    if (Photo != null)
                    {
                        if (Book.BookPhoto != null)
                        {
                            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/bookImages/", Book.BookPhoto);
                            System.IO.File.Delete(filePath);
                        }
                        Book.BookPhoto = UploadBookPhoto();
                    }
                    Book = bookService.UpdateBook(Book);
                    TempData["Messge"] = "Book Updated!";
                }

                bookService.Commit();

                return RedirectToPage("BookDetails", new { id = Book.Id });
            }


            SelectAuthor = authorService.GetAuthors().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            SelectPublisher = publisherService.GetPublishers().Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            return Page();
        }
        private string UploadBookPhoto()
        {
            string uniqueName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/bookimages");
                uniqueName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueName;
        }
        //private TitleAuthor CheckMultipleTitleAuthor(int titleAuthor)
        //{
        //    var newTitleAuthor = new TitleAuthor();
        //    if (titleAuthor != 0)
        //    {
        //        newTitleAuthor = titleAuthorService.GetAuthorForTitleAuthor(titleAuthor);
        //    }
        //    return newTitleAuthor;
            
        //}
    }
}