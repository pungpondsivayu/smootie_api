using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomRequest
{
    public class IngredientRequest
    {
        public int IngredientId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Unit { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsUsed { get; set; }
    }
}
