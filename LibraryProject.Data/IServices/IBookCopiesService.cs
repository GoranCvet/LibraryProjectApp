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
        BookCopies GetBookCopyById(int bookId, int libraryId);
        BookCopies CreateBookCopy(BookCopies bookCopy);
        BookCopies DeleteCopy(int bookId, int libraryId);
        BookCopies UpdateCopy(BookCopies bookCopy);
    }
}
