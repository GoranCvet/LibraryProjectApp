using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Domain
{
    public class BookCopies
    {
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Library Library { get; set; }
        public int LibraryId { get; set; }

        [Required]
        public int NumberOfCopies { get; set; }

       

    }
}
