using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp30.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsFamily { get; set; }
        public bool IsFriend { get; set; }
        public bool IsWork { get; set; }
    }
}
