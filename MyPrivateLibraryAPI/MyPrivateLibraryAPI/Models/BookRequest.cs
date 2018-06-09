using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPrivateLibraryAPI.Models
{
    public class BookRequest
    {
        public int Id { get; set; }

        [TitleValidation(minLength = 3, maxLength = 50)]
        public string Title { get; set; }

        [YearValidation(min = 0, max = 2050)]
        public int PublicationYear { get; set; }

        [DateTimeValidation(allowNull = true, minYear = 1990, maxYear = 2050)]
        public DateTime? ReadingStart { get; set; }

        [DateTimeValidation(allowNull = true, minYear = 1990, maxYear = 2050)]
        public DateTime? ReadingEnd { get; set; }
    }
}
