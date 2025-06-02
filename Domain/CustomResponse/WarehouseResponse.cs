namespace Domain.CustomResponse
{
    public class StockResponse
    {
        public int StockId { get; set; }

        public int? BranchId { get; set; }

        public int? IngredientId { get; set; }

        public decimal? Quantity { get; set; }
        public ShotIngredientResponse Ingredient { get; set; }
    }
    
    public class ShotIngredientResponse
    {
        public string Name { get; set; }
        public string Unit { get; set; }
    }


    public class StockRequestResponse
    {
        public int RequestId { get; set; }

        public int? EmployeeId { get; set; }

        public int? BranchId { get; set; }

        public DateTime? RequestDate { get; set; }

        public string Status { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public string RequestType { get; set; }

        public List<StockRequestItemResponse> StockRequestItem { get; set; }
    }

    public class StockRequestItemResponse
    {
        public int ItemId { get; set; }

        public string? Name { get; set; }

        public string? Unit { get; set; }

        public decimal? Quantity { get; set; }
    }

    public class CouterStockResponse
    {
        public int CounterStockId { get; set; }

        public int BranchId { get; set; }
        
        public string Name { get; set; }

        public string Unit { get; set; }
        public int IngredientId { get; set; }

        public decimal Quantity { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
