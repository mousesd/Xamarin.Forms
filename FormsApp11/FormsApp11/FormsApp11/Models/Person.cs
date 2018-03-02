using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp11
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }

        public Person(string name, int age, string location)
        {
            this.Name = name;
            this.Age = age;
            this.Location = location;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
