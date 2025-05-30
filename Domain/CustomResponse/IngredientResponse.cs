namespace Domain.CustomResponse
{
    public class IngredientResponse
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }        
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsUsed { get; set; }
    }
}
