using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using WebAppWithMVC.Models.Publihsers;

namespace WebAppWithMVC.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }
        public IActionResult Index()
        {
            var model = new PublisherIndexViewModel();
            model.Publishers = publisherService.GetPublishers();
            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            var publisher = new Publisher();
            if (id.HasValue)
            {
                publisher = publisherService.GetPublisherById(id.Value);
                if(publisher == null)
                {
                    return RedirectToAction("NotFound");
                }
            }

            return View(publisher);
           
        }
        [HttpPost]
        public IActionResult Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if(publisher.Id == 0)
                {
                    publisher = publisherService.CreatePublisher(publisher);
                    TempData["Message"] = "Publisher Created";
                }
                else
                {
                    publisher = publisherService.UpdatePublisher(publisher);
                    TempData["Message"] = "Publisher Updated!";
                }

                publisherService.Commit();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var publisher = publisherService.DeletePublisher(id);
            if(publisher == null)
            {
                return RedirectToAction("NotFound");
            }

            publisherService.Commit();
            TempData["Message"] = "Publisher Deleted!";

            return RedirectToAction("Index");

        }


    }
}