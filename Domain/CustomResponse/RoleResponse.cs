using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomResponse
{
    public class RoleResponse
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public bool? IsUsed { get; set; }
    }
}
