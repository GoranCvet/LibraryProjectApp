using LibraryProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Data.IServices
{
    public interface ILendingService
    {
        IEnumerable<Lending> GetLendings();
        int Commit();
        Lending CreateLending(Lending lending);
        Lending GetLendingById(int id);
        Lending ReturnLentBook(Lending lending);
    }
}
