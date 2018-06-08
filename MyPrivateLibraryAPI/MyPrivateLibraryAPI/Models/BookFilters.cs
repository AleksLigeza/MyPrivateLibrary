using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPrivateLibraryAPI.Models
{
    public class BookFilters
    {
        public int? PublicationYearSince { get; set; }
        public int? PublicationYearTo { get; set; }
        public string Title { get; set; }
        public bool? Read { get; set; }
        public bool? CurrentlyReading { get; set; }
    }
}
