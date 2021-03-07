using System;
using System.ComponentModel.DataAnnotations;

namespace recipes.Models
{
    public class Ingredient
    {
        public Guid id { get; set; }
        public Guid recipeId { get; set; }

        [Required(ErrorMessage = "Ingredient name is required.")]
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}
