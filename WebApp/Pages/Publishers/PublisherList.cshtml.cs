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
    public class PublisherListModel : PageModel
    {
        private readonly IPublisherService publisherService;

        public PublisherListModel(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }
        [TempData]
        public string Message { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public void OnGet()
        {
            Publishers = publisherService.GetPublishers();
        }
    }
}