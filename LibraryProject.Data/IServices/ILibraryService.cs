using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface ILibraryService
    {
        IEnumerable<Library> GetLibraries();
        int Commit();
        Library GetLibraryById(int id);
        Library CreateLibrary(Library library);
        Library UpdateLibrary(Library library);
        Library DeleteLibrary(int id);
    }
}
