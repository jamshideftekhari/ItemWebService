using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DRMusic.Models
{
    public class Music
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationYear { get; set; }
        public double Duration { get; set; }
    }
}
