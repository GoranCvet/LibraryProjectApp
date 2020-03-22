using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Domain
{
    public class Book
    {
        public Book()
        {
            Lendings = new List<Lending>();
            BookCopies = new List<BookCopies>();
        }
        public int Id { get; set; }
        [Required, Display(Name = "Book Title")]
        public string Title { get; set; }
        public int YearOfIssue { get; set; }
        public int NumberOfPages { get; set; }
        public string BookPhoto { get; set; }
        [Required, Display(Name = "Book Description")]
        public string BookDescription { get; set; }

        public Author Author { get; set; }
        [Display(Name ="Author")]
        public int AuthorId { get; set; }
        public Publisher Publisher { get; set; }
        [Display(Name = "Publihser")]
        public int PublisherId { get; set; }

        public List<Lending> Lendings { get; set; }
        public List<BookCopies> BookCopies { get; set; }
    }
}
