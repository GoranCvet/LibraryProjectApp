﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp
{
    public class BookCopyEditModel : PageModel
    {
        private readonly IBookCopiesService bookCopiesService;
        private readonly ILibraryService libraryService;

        public BookCopyEditModel(IBookCopiesService bookCopiesService)
        {
            this.bookCopiesService = bookCopiesService;
        }
        [BindProperty]
        public BookCopies BookCopy { get; set; }
        public IActionResult OnGet(int bookId, int libraryId)
        {
            BookCopy = bookCopiesService.GetBookCopyById(bookId, libraryId);
            if (BookCopy == null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                BookCopy = bookCopiesService.UpdateCopy(BookCopy);
                bookCopiesService.Commit();

                TempData["Message"] = "Changes in copies applied!";

                return RedirectToPage("BookCopiesList");
            }

            return Page();
        }
    }
}