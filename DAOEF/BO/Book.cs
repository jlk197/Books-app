using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlechammerKardas.Books.DAOEF.BO
{
    public class Book : Interfaces.IBook
    {
        public int Id { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        [NotMapped]
        IProducer IBook.Producer { 
            get=>Producer; 
            set {
                this.Producer= value as BO.Producer;
                this.ProducerId = value.Id;
            } 
        }
        public int ProdYear { get; set; }
        public string Title { get; set; }
        public GenreType Genre { get; set; }
        public int PublicationYear { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Producer.Name} {Title}, {Genre}";
        }


    }
}
