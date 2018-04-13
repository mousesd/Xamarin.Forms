namespace FormsApp21.Validations
{
    public class PasswordValidator : IValidationRule<string>
    {
        private static readonly int MinLength = 6;

        #region == Impelment members of the IValidationRule<T> interface ==
        public string Description
        {
            get { return $"Password should at least {MinLength} characters long."; }
        }

        public bool Validate(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Length >= MinLength;
        } 
        #endregion
    }
}
