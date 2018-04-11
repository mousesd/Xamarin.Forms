namespace FormsApp20.Validations
{
    public class PasswordValidator : IValidationRule<string>
    {
        private static readonly int MinLength = 2;

        public string Description
        {
            get { return $"Password should be at least {MinLength} characters long."; }
        }

        public bool Validate(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Length >= MinLength;
        }
    }
}
