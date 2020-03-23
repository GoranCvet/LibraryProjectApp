using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithMVC.Models.Publihsers
{
    public class PublisherIndexViewModel
    {
        public string Message { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }

    }
}
