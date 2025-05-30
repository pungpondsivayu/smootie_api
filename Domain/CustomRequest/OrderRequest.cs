namespace Domain.CustomRequest
{

    public class OrderRequest
    {
        public int? CustomerId { get; set; }
        public int BranchId { get; set; }
        public DateTime OrderTime { get; set; }
        public string Channel { get; set; }
        public string PaymentMethod { get; set; }
        public string PickupMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public int? CouponId { get; set; }
        public int PointsEarned { get; set; }
        public int PointsUsed { get; set; }
        public string Status { get; set; }
        public DateTime? PickupTime { get; set; }
        public string DeliveryStatus { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; } = new();
    }

    public class OrderItemRequest
    {
        public int? OrderId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
