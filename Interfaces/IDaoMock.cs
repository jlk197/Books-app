using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlechammerKardas.Books.Interfaces
{
    public interface IDaoMock
    {
        IEnumerable<IBook> GetAllBooks();
        IEnumerable<IProducer> GetAllProducers();

        IBook CreateNewBook();
        IProducer CreateNewProducent();

        void SaveChanges();
        void AddBook(IBook book);
        void RemoveBook(IBook book);
        void AddProducer(IProducer producent);
        void RemoveProducer(IProducer producent);
    }
}
