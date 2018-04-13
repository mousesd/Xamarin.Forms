using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MvvmHelpers;

namespace FormsApp21.Validations
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class ValidatableObject<T> : BaseViewModel
    {
        private readonly Action propertyChangedCallback;

        public List<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();

        private T _value;
        public T Value
        {
            get { return _value; }
            set { this.SetProperty(ref _value, value, onChanged: propertyChangedCallback); }
        }

        public bool IsValid
        {
            get { return this.Validations.TrueForAll(r => r.Validate(_value)); }
        }

        public string ValidationDescriptions
        {
            get { return string.Join(Environment.NewLine, this.Validations.Select(r => r.Description)); }
        }

        public ValidatableObject(Action propertyChangedCallback, T value, params IValidationRule<T>[] validations)
        {
            this.propertyChangedCallback = propertyChangedCallback;
            _value = value;
            this.Validations.AddRange(validations);
        }
    }
}
