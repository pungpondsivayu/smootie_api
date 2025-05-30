using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomRequest
{
    public class TransectionRequest
    {
        public int RequestId { get; set; }

        [Required]
        public int? EmployeeId { get; set; }

        [Required]
        public int? BranchId { get; set; }

        public DateTime? RequestDate { get; set; }

        public string Status { get; set; }

        public string RequestType { get; set; }
        public List<StockItemRequest> stockItem { get; set; }
    }

    public class StockItemRequest
    {
        public int StockId { get; set; }

        public int? IngredientId { get; set; }

        public decimal? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }
    }

}
