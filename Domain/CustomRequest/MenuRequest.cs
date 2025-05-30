using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.CustomRequest
{
    public class MenuRequest
    {
        
        public int MenuId { get; set; }
        [Required]

        public string Name { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsUsed { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? CategoryId { get; set; }
    }

    public class MenuCategoryRequest
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public bool? IsUsed { get; set; }
    }

    public class MenuRecipeRequest
    {
        public int RecipeId { get; set; }
        [Required]
        public int? MenuId { get; set; }

        [Required]
        public int? IngredientId { get; set; }

        [Required]
        public decimal? Quantity { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
