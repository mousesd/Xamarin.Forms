using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FormsApp20.Validations
{
    public class EmailValidator : IValidationRule<string>
    {
        private static readonly string Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" 
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public string Description
        {
            get { return "Please enter a valid email."; }
        }

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var regex = new Regex(Pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(value);
        }
    }
}
