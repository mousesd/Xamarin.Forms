using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;

namespace FormsApp11
{
    public class PersonListViewModel : BaseViewModel
    {
        private ObservableRangeCollection<Person> persons;
        public ObservableRangeCollection<Person> Persons
        {
            get { return persons; }
            set
            {
                this.persons = value;
                this.SetProperty(ref persons, value);
            }
        }

        public PersonListViewModel()
        {
            this.Title = "Person List";
            this.Persons = new ObservableRangeCollection<Person>
            {
                new Person("Steve", 21, "USA"),
                new Person("John", 37, "USA"),
                new Person("Tom", 42, "UK"),
                new Person("Lucas", 29, "Germany"),
                new Person("Tariq", 39, "UK"),
                new Person("Jane", 30, "USA")
            };
        }
    }
}
