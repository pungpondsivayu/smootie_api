namespace Domain.CustomRequest
{
    public class RoleRequest
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public bool? IsUsed { get; set; }
    }
}
