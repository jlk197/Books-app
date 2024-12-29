using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBook
    {
        int Id { get; set; }
        string Title { get; set; }
        IProducer Producer { get; set; }
        GenreType Genre { get; set; }
        int PublicationYear { get; set; }
    }
}
