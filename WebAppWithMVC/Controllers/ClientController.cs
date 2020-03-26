using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using WebAppWithMVC.Models.Clients;

namespace WebAppWithMVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public IActionResult Index(string SearchTerm = null)
        {
            var model = new ClientIndexViewModel();
            model.Clients = clientService.GetClients(SearchTerm);
            return View(model);
        }
        public IActionResult ClientReturnedBooks(int id)
        {
            var client = clientService.GetClientById(id);
            return View(client);
        }
        public IActionResult Edit(int? id)
        {
            var client = new Client();
            if (id.HasValue)
            {
                client = clientService.GetClientById(id.Value);
                if (client == null)
                {
                    return View("NotFound");
                }
            }

            return View(client);
        }
        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                if(client.Id == 0)
                {
                    client = clientService.CreateClient(client);
                    TempData["Message"] = "New Client created!";
                }
                else
                {
                    client = clientService.UpdateClient(client);
                    TempData["Message"] = "Client Updated";
                }
                clientService.Commit();
                return RedirectToAction("Index");
            }

            return View(client);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var client = clientService.DeleteClient(id);
            if(client == null)
            {
                return View("NotFound");
            }
            clientService.Commit();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var client = clientService.GetClientById(id);
            if(client == null)
            {
                return View("NotFound");
            }
            return View(client);
        }

    }
}