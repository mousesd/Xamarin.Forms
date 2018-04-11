namespace FormsApp20.Validations
{
    public interface IValidationRule<in T>
    {
        string Description { get; }
        bool Validate(T value);
    }
}
