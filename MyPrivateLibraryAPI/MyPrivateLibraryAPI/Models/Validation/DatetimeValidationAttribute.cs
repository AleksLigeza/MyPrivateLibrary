using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyPrivateLibraryAPI.Models
{
    public class DateTimeValidationAttribute : ValidationAttribute
    {
        public int minYear { get; set; }
        public int maxYear { get; set; }
        public bool allowNull { get; set; }

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                if(allowNull)
                {
                    return true;
                }
                return false;
            }

            var minDate = new DateTime(minYear, 1, 1);
            var maxDate = new DateTime(maxYear, 1, 1);

            var dateTime = Convert.ToDateTime(value);

            if (dateTime < minDate || dateTime > maxDate)
            {
                return false;
            }

            return true;
        }
    }
}
