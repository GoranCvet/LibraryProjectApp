using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Domain
{
    public class BookCopiesLibrary
    {
        public BookCopies BookCopies { get; set; }
        public int BookCopiesId { get; set; }

        public Library Library { get; set; }
        public int LibraryId { get; set; }

        public int LibriryCopies { get; set; }
    }
}
