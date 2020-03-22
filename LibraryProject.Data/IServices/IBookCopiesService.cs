using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface IBookCopiesService
    {
        IEnumerable<BookCopies> GetBookCopies();
        int Commit();
        //BookCopies GetBookCopyById(int id);
        BookCopies CreateBookCopy(BookCopies bookCopy);
        //BookCopies DeleteCopy(int id);
        //BookCopies UpdateCopy(BookCopies bookCopy);
    }
}
