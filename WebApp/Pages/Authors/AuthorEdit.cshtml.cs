using System;
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

namespace WebApp
{
    public class AuthorEditModel : PageModel
    {
        private readonly IAuthorService authorService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AuthorEditModel(IAuthorService authorService, IWebHostEnvironment webHostEnvironment)
        {
            this.authorService = authorService;
            this.webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public Author Author { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Author = authorService.GetAuthorById(id.Value);
                if(Author == null)
                {
                    return RedirectToPage("NotFound");
                }
            }
            else
            {
                Author = new Author();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Author.Id == 0)
                {
                    if (Photo != null)
                    {
                        if (Author.AuthorPhoto != null)
                        {
                            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/authorImages/", Author.AuthorPhoto);
                            System.IO.File.Delete(filePath);
                        }
                        Author.AuthorPhoto = UploadAuthorPhoto();
                    }
                    Author = authorService.CreateAuthor(Author);
                    TempData["Message"] = "Author Created!";
                }
                else
                {
                    if (Photo != null)
                    {
                        if (Author.AuthorPhoto != null)
                        {
                            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/authorImages/", Author.AuthorPhoto);
                            System.IO.File.Delete(filePath);
                        }
                        Author.AuthorPhoto = UploadAuthorPhoto();
                    }
                    Author = authorService.UpdateAuthor(Author);
                    TempData["Message"] = "Author Updated!";
                }

                authorService.Commit();

                return RedirectToPage("AuthorDetails", new { id = Author.Id });
            }

            return Page();
        }
        private string UploadAuthorPhoto()
        {
            string uniqueName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/authorImages/");
                uniqueName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueName;
        }
    }
}


        
