using System;
using System.ComponentModel.DataAnnotations;

namespace recipes.Models
{
    public class Recipe
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "Recipe name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
