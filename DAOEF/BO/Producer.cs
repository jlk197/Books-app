using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOEF.BO
{
    public class Producer : Interfaces.IProducer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Długość nazwy <1, 30>")]
        public string Name { get; set; }
       
        public List<Book> Books { get; set; } = new List<Book>();

        [Required(ErrorMessage = "Kraj musi zostać podany")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Długość nazwy kraju <1, 20>")]
        public string Country { get; set; }
        [Required]
        [Range(1900, 2025, ErrorMessage = "Rok założenia <1900, 2025>")]
        public int EstablishmentYear { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}
