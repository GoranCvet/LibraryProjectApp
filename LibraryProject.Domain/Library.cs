using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Domain
{
    public class Library
    {
        public Library()
        {
            BookCopiesLibraries = new List<BookCopiesLibrary>();
            Lendings = new List<Lending>();
        }
        public int Id { get; set; }
        [Required, Display(Name = "Library Name"), StringLength(50, ErrorMessage = "Name length can't be more than 50")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }

        public List<BookCopiesLibrary> BookCopiesLibraries { get; set; }
        public List<Lending> Lendings { get; set; }

    }
}
