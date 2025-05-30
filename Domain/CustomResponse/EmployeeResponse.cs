using Domain.Entities;

namespace Domain.CustomResponse
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }

        public int? BranchId { get; set; }

        public int? RoleId { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public DateTime? HireDate { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public bool? IsUsed { get; set; }

        public string Image { get; set; }
        public BranchShortResponse Branch { get; set; } // แทน MenuCategoryResponse
        public RoleShortResponse Role { get; set; } // แทน MenuCategoryResponse
    }

    public class BranchShortResponse
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
    }

    public class RoleShortResponse
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
