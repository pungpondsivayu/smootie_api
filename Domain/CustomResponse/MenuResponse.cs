using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.CustomResponse
{
    public class MenuResponse
    {
        public int MenuId { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsUsed { get; set; }
        public string Image { get; set; }
        public int? CategoryId { get; set; }
        public CategoryShortResponse Category { get; set; } // แทน MenuCategoryResponse

    }

    public class MenuCategoryResponse
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsUsed { get; set; }
    }

    public class CategoryShortResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
