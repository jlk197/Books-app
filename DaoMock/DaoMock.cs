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
            bookList.Add(book);
        }

        public void AddProducer(IProducer producent)
        {
            producerList.Add(producent);
        }

        public IBook CreateNewBook()
        {
            return new Book();
        }

        public IProducer CreateNewProducent()
        {
            return new Producer();
        }

        public List<IBook> GetAllBooks()
        {
            return bookList;
        }


        public IEnumerable<IProducer> GetAllProducers()
        {
            return producerList;
        }

        public bool ProducerHasBooks(IProducer producent)
        {
            return bookList.Any(b => b.Producer.Id == producent.Id);
        }

        public void RemoveBook(IBook book)
        {
            bookList.Remove(book);
        }

        public void RemoveProducer(IProducer producent)
        {
            producerList.Remove(producent);
        }

        public void SaveChanges()
        {
            
        }

        IEnumerable<IBook> IDaoMock.GetAllBooks()
        {
            return bookList;
        }
    }
}
