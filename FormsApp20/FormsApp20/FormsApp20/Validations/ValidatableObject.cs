using System;
using System.Collections.Generic;
using System.Linq;
using PropertyChanged;

using FormsApp20.ViewModels;

namespace FormsApp20.Validations
{
    public class ValidatableObject<T> : BaseViewModel
    {
        private readonly Action propertyChangedCallback;

        public List<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();
        public T Value { get; set; }

        [DependsOn(nameof(Value))]
        public bool IsValid
        {
            get { return this.Validations.TrueForAll(v => v.Validate(this.Value)); }
        }

        public string ValidationDescriptions
        {
            get { return string.Join(Environment.NewLine, this.Validations.Select(v => v.Description)); }
        }

        public ValidatableObject(Action propertyChangedCallback = null, params IValidationRule<T>[] validations)
        {
            this.propertyChangedCallback = propertyChangedCallback;
            this.Validations.AddRange(validations);
        }

        private void OnValueChanged()
        {
            propertyChangedCallback?.Invoke();
        }
    }
}
