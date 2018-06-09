using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPrivateLibraryAPI.Models
{
    public class YearValidationAttribute : ValidationAttribute
    {
        public int min { get; set; }
        public int max { get; set; }

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }

            var year = int.Parse(value.ToString());

            if(year < min || year > max)
            {
                return false;
            }

            return true;
        }
    }
}
