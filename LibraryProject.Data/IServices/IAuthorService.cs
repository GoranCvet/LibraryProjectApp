using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAuthors(string input = null);
        Author GetAuthorById(int id);
        int Commit();
        Author CreateAuthor(Author author);
        Author UpdateAuthor(Author author);
        Author DeleteAuthor(int id);
    }
}
