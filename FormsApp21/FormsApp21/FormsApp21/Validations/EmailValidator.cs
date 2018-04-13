using System.Text.RegularExpressions;

namespace FormsApp21.Validations
{
    public class EmailValidator : IValidationRule<string>
    {
        private static readonly string Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" 
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        #region == Impelment members of the IValidationRule<T> interface ==
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
        #endregion
    }
}
