using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KlechammerKardas.Books.DaoMock
{
    public class Book : Interfaces.IBook
    {
        public int Id { get; set; }
        public int ProdYear { get; set; }
        public string Title { get; set; }
        public IProducer Producer { get; set; }
        public GenreType Genre { get; set; }
        public int PublicationYear { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Producer.Name} {Title}, {Genre}";
        }
    }
}
