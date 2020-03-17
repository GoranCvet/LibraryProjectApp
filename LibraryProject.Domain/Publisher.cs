using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Domain
{
    public class Publisher
    {
        public Publisher()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }
        [Required, Display(Name="Publisher Name"), StringLength(30, ErrorMessage = "Name length can't be more than 30")]
        public string Name { get; set; }
        public string Country { get; set; }

        public List<Book> Books { get; set; }
    }
}
