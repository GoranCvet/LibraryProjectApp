using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Domain
{
    public class Author
    {
        public Author()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }
        [Required, Display(Name = "Author Name"), StringLength(50, ErrorMessage = "Name length can't be more than 50")]
        public string Name { get; set; }
        public string Country { get; set; }
        [Required, Display(Name="Author Biography")]
        public string AuthorBio { get; set; }
        public string AuthorPhoto { get; set; }

        public List<Book> Books { get; set; }
    }
}
