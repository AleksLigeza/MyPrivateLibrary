using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPrivateLibraryAPI.Models
{
    public class BookFilters
    {
        [YearValidation(min = 0, max = 2100)]
        public int? PublicationYearSince { get; set; }

        [YearValidation(min = 0, max = 2100)]
        public int? PublicationYearTo { get; set; }

        [TitleValidation(minLength = 0, maxLength = 50)]
        public string Title { get; set; }

        public bool Read { get; set; }
        public bool CurrentlyReading { get; set; }

        public OrderByFiled order { get; set; }

    }

    public enum OrderByFiled
    {
        OrderByTitle,
        OrderByTitleDesc,
        OrderByYear,
        OrderByYearDesc
    }
}
