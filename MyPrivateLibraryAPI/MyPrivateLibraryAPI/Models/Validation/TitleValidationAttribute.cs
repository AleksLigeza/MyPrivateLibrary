using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPrivateLibraryAPI.Models
{
    public class TitleValidationAttribute : ValidationAttribute
    {
        public int maxLength { get; set; }
        public int minLength { get; set; }

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }

            if (value.ToString().Length < minLength || value.ToString().Length > maxLength)
            {
                return false;
            }

            return true;
        }
    }
}
