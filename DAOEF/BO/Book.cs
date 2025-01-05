using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOEF.BO
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
                if (value != null) {
                    this.Producer = value as BO.Producer;
                    this.ProducerId = value.Id;
                }
            } 
        }
        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Długość nazwy <1, 30>")]
        public string Title { get; set; }
        public GenreType Genre { get; set; }
        [Range(1850, 2025, ErrorMessage = "Rok wydania <1850, 2025>")]
        public int PublicationYear { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Producer.Name} {Title}, {Genre}";
        }


    }
}
