using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomResponse
{
    public class BranchResponse
    {
        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string SubDistrict { get; set; }

        public string PostalCode { get; set; }

        public string AddressDetail { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        public bool? IsUsed { get; set; }

        //public int? employeeId { get; set; }
    }
}
