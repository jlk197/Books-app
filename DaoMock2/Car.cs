using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DaoMock
{
    public class Car : Interfaces.IBook
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public IProducent Producent { get; set; }
        public TransmissionType Transmission { get; set; }
        public int ProdYear { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Producent.Name} {Model}, {Transmission}";
        }
    }
}
