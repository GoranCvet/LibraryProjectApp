using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebAppWithMVC.Models.Authors;

namespace WebAppWithMVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AuthorController(IAuthorService authorService, IWebHostEnvironment webHostEnvironment)
        {
            this.authorService = authorService;
            this.webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Index(string SearchAuthor = null)
        {
            var model = new AuthorsIndexViewModel();
            model.Authors = authorService.GetAuthors(SearchAuthor);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = new AuthorDetailsViewModel();
            model.Author = authorService.GetAuthorById(id);
            if(model.Author == null)
            {
                return View("NotFound");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var model = new AuthorEditViewModelcs();
            if (id.HasValue)
            {
                model.Author = authorService.GetAuthorById(id.Value);
                if(model.Author == null)
                {
                    return View("NotFound");
                }
            }
            else
            {
                model.Author = new Author();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AuthorEditViewModelcs model)
        {
            if (ModelState.IsValid)
            {
                if(model.Author.Id == 0)
                {
                        if (model.Photo != null)
                        {
                            if (model.Author.AuthorPhoto != null)
                            {
                                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/authorImages/", model.Author.AuthorPhoto);
                                System.IO.File.Delete(filePath);
                            }
                            model.Author.AuthorPhoto = UploadAuthorPhoto(model);
                        }
                        model.Author = authorService.CreateAuthor(model.Author);

                        TempData["Message"] = "New Author Created!";
                }
                else
                {
                    if (model.Photo != null)
                    {
                        if (model.Author.AuthorPhoto != null)
                        {
                            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/authorImages/", model.Author.AuthorPhoto);
                            System.IO.File.Delete(filePath);
                        }
                        model.Author.AuthorPhoto = UploadAuthorPhoto(model);
                    }

                    model.Author = authorService.UpdateAuthor(model.Author);

                    TempData["Message"] = "Author updated!";
                }

                authorService.Commit();

                return RedirectToAction("Details", new { id = model.Author.Id });

            }

            return View(model);
        }
        private string UploadAuthorPhoto(AuthorEditViewModelcs model)
        {
            string uniqueName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/authorImages/");
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
            var model = authorService.DeleteAuthor(id);
            if(model == null)
            {
                return View("NotFound");
            }
            authorService.Commit();
            TempData["Message"] = "Author Deleted!";

            return RedirectToAction("Index");
        }
    }
}