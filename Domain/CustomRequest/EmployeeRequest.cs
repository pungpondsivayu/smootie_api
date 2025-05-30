using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.CustomRequest
{
    public class EmployeeRequest
    { 
        public int EmployeeId { get; set; }
        [Required]
        public int? BranchId { get; set; }

        [Required]
        public int? RoleId { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime? HireDate { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public bool? IsUsed { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
