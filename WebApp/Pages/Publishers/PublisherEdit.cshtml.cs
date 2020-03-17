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
    public class PublisherEditModel : PageModel
    {
        private readonly IPublisherService publisherService;

        public PublisherEditModel(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }
        [BindProperty]
        public Publisher Publisher { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Publisher = publisherService.GetPublisherById(id.Value);
                if(Publisher == null)
                {
                    return RedirectToPage("NotFound");
                }
            }
            else
            {
                Publisher = new Publisher();
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Publisher.Id == 0)
                {
                    Publisher = publisherService.CreatePublisher(Publisher);
                    TempData["Message"] = "Publisher Created!";
                }
                else
                {
                    Publisher = publisherService.UpdatePublisher(Publisher);
                    TempData["Message"] = "Publisher Upadated!";
                }
                publisherService.Commit();
                return RedirectToPage("PublisherList", new { id = Publisher.Id });
            }

            return Page();
        }
    }
}