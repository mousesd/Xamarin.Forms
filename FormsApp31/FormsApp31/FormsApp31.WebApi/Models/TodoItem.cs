using System.ComponentModel.DataAnnotations;

namespace FormsApp31.WebApi.Models
{
    public class TodoItem
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Notes { get; set; }

        public bool Done { get; set; }
    }
}
