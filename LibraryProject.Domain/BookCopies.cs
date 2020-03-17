using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Domain
{
    public class BookCopies
    {
        public BookCopies()
        {
            BookCopiesLibraries = new List<BookCopiesLibrary>();
        }
        public int Id { get; set; }
        [Required]
        public int NumberOfCopies { get; set; }

        public Book Book { get; set; }
        public int BookId { get; set; }

        public List<BookCopiesLibrary> BookCopiesLibraries { get; set; }
    }
}
