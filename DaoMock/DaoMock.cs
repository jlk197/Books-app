using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoMock
{
    public class DaoMock : Interfaces.IDaoMock
    {
        List<IBook> bookList;
        List<IProducer> producerList;
        public DaoMock()
        {
            producerList = new List<IProducer>()
        {
            new Producer() { Id = 1, Name = "Znak", Country = "Polska", EstablishmentYear = 1959 },
            new Producer() { Id = 2, Name = "Wydawnictwo Literackie", Country = "Polska", EstablishmentYear = 1953 }
        };

            bookList = new List<IBook>()
        {
            new Book() { Id = 1, Title = "Lalka", Producer = producerList[0], Genre = GenreType.Romance, PublicationYear = 1890 },
            new Book() { Id = 2, Title = "Ogniem i mieczem", Producer = producerList[0], Genre = GenreType.Fantasy, PublicationYear = 1884 },
            new Book() { Id = 3, Title = "Zbrodnia i kara", Producer = producerList[1], Genre = GenreType.Crime, PublicationYear = 1866 },
            new Book() { Id = 4, Title = "Solaris", Producer = producerList[1], Genre = GenreType.ScienceFiction, PublicationYear = 1961 }
        };
        }

        public void AddBook(IBook book)
        {
            throw new NotImplementedException();
        }

        public void AddProducer(IProducer producent)
        {
            throw new NotImplementedException();
        }

        public IBook CreateNewBook()
        {
            throw new NotImplementedException();
        }

        public IProducer CreateNewProducent()
        {
            throw new NotImplementedException();
        }

        public List<IBook> GetAllBooks()
        {
            return bookList;
        }

        public List<IProducer> GetAllProducents()
        {
            return producerList;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            throw new NotImplementedException();
        }

        public void RemoveBook(IBook book)
        {
            throw new NotImplementedException();
        }

        public void RemoveProducer(IProducer producent)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IBook> IDaoMock.GetAllBooks()
        {
            throw new NotImplementedException();
        }
    }
}
