using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPrivateLibraryAPI.DbModels
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PublicationYear { get; set; }

        public DateTime? ReadingStart { get; set; }
        public DateTime? ReadingEnd { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
