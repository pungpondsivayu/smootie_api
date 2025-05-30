using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomRequest
{
    public class BranchRequest
    {
        public int BranchId { get; set; }

        [Required] 
        public string Province { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string SubDistrict { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public string AddressDetail { get; set; }

        public string CreatedBy { get; set; }

        public bool? IsUsed { get; set; }
    }
}
