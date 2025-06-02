namespace Domain.CustomResponse;

public class OrderResponse
{
    public int OrderId { get; set; }
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
    public List<OrderItemResponse> OrderItems { get; set; }
}

public class OrderItemResponse
{
    public int? OrderId { get; set; }
    public int MenuId { get; set; }
    public string Image {  get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}