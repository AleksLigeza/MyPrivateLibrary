using System;

namespace MyPrivateLibraryAPI.Models
{
    public class BookResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int PublicationYear { get; set; }

        public DateTime? ReadingStart { get; set; }
        public DateTime? ReadingEnd { get; set; }
        public string UserId { get; set; }
    }
}
