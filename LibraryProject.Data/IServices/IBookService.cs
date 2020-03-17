using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks(string book = null, string author = null);
        int Commit();
        Book GetBookById(int id);
        Book CreateBook(Book book);
        Book UpdateBook(Book book);
        Book Delete(int id);
    }
}
