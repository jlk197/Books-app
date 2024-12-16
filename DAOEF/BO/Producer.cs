using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOEF.BO
{
    public class Producer : Interfaces.IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; }
        public string Country { get; set; }
        public int EstablishmentYear { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}
