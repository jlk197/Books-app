using Interfaces;
using System.Reflection;
using System.Configuration;
using KlechammerKardas.Books.Interfaces;
using KlechammerKardas.Books.BLC;

namespace KlechammerKardas.Books.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string liblaryName = ConfigurationManager.AppSettings["library_name"];
            Interfaces.IDaoMock daoMock = new BLC.BLC(liblaryName).DAO;

           /* IProducer newProducer =  daoMock.CreateNewProducent();
            newProducer.Id = 1;
            newProducer.Name = "Znak";
            newProducer.Country = "Polska";
            newProducer.EstablishmentYear = 1959;
            daoMock.AddProducer(newProducer);
            daoMock.SaveChanges();

            IProducer newProducer2 = daoMock.CreateNewProducent();
            newProducer2.Id = 2;
            newProducer2.Name = "Wydawnictwo Literackie";
            newProducer2.Country = "Polska";
            newProducer2.EstablishmentYear = 1953;
            daoMock.AddProducer(newProducer2);
            daoMock.SaveChanges();

            IBook newBook1 = daoMock.CreateNewBook();
            newBook1.Id = 1;
            newBook1.Title = "Lalka";
            newBook1.Producer = newProducer; 
            newBook1.Genre = GenreType.Romance;
            newBook1.PublicationYear = 1890;
            daoMock.AddBook(newBook1);
            daoMock.SaveChanges();

            IBook newBook2 = daoMock.CreateNewBook();
            newBook2.Id = 2;
            newBook2.Title = "Ogniem i mieczem";
            newBook2.Producer = newProducer; 
            newBook2.Genre = GenreType.Fantasy;
            newBook2.PublicationYear = 1884;
            daoMock.AddBook(newBook2);
            daoMock.SaveChanges();

            IBook newBook3 = daoMock.CreateNewBook();
            newBook3.Id = 3;
            newBook3.Title = "Zbrodnia i kara";
            newBook3.Producer = newProducer2; 
            newBook3.Genre = GenreType.Crime;
            newBook3.PublicationYear = 1866;
            daoMock.AddBook(newBook3);
            daoMock.SaveChanges();

            IBook newBook4 = daoMock.CreateNewBook();
            newBook4.Id = 4;
            newBook4.Title = "Solaris";
            newBook4.Producer = newProducer2; 
            newBook4.Genre = GenreType.ScienceFiction;
            newBook4.PublicationYear = 1961;
            daoMock.AddBook(newBook4);
            daoMock.SaveChanges();*/

            Console.WriteLine("### PRODUCERS ###");
            foreach (IProducer p in daoMock.GetAllProducers())
            {
                Console.WriteLine(p.ToString());
            }

            Console.WriteLine("### BOOKS ###");
            foreach (IBook c in daoMock.GetAllBooks())
            {
                Console.WriteLine(c.ToString());
            }
        }
    }
}
