using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface IBookCopiesLibraryService
    {
        BookCopiesLibrary Create(BookCopiesLibrary bookCopiesLibrary);
        int Commit();
        IEnumerable<BookCopiesLibrary> Get();
    }
}
