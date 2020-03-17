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
    public class PublisherDeleteModel : PageModel
    {
        private readonly IPublisherService publisherService;

        public PublisherDeleteModel(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }
        [BindProperty]
        public Publisher Publisher { get; set; }
        public IActionResult OnGet(int id)
        {
            Publisher = publisherService.GetPublisherById(id);
            if(Publisher == null)
            {
                return RedirectToPage("NotFound");
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            var pub = publisherService.DeletePublisher(Publisher.Id); 
            if(pub == null)
            {
                return RedirectToPage("NotFound");
            }
            publisherService.Commit();
            TempData["Message"] = "Publisher Deleted!";

            return RedirectToPage("PublisherList");
        }
    }
}