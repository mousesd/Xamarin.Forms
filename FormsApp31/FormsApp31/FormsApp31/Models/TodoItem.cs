using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp31.Models
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }
}
