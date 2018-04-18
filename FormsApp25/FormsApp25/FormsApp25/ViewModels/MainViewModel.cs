using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using FormsApp25.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace FormsApp25.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Person> People { get; }
        public ICommand OutputAgeCommand { get; }

        private string selectedItemText;
        public string SelectedItemText
        {
            get { return selectedItemText; }
            set { this.SetProperty(ref selectedItemText, value); }
        }

        public MainViewModel()
        {
            this.People = new ObservableCollection<Person>
            {
                new Person ("Steve", 21),
                new Person ("John", 37),
                new Person ("Tom", 42),
                new Person ("Lucas", 29),
                new Person ("Tariq", 39),
                new Person ("Jane", 30)
            };
            this.OutputAgeCommand = new Command<Person>(this.OutputAge);
        }

        private void OutputAge(Person person)
        {
            this.SelectedItemText = $"{person.Name} is {person.Age} years old.";
        }
    }
}
