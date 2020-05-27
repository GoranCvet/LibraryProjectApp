using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface ITitleAuthorService
    {
        TitleAuthor GetAuthorForTitleAuthor(int id);
        int Commit();
    }
}
