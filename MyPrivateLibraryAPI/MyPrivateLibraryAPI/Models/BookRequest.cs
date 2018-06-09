using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPrivateLibraryAPI.Models
{
    public class BookRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int PublicationYear { get; set; }

        public DateTime? ReadingStart { get; set; }
        public DateTime? ReadingEnd { get; set; }
    }
}
