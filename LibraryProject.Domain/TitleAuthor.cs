using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace LibraryProject.Domain
{
    public class TitleAuthor
    {
        public Author Author { get; set; }
        public int? AuthorId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
