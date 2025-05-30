using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace Application.helper
{
    public class UserCreator
    {
        public readonly IServiceFactory _service;

        public UserCreator(IServiceFactory service)
        {
            _service = service; 
        }


        public string getCreatorEmployee(int id)
        {
            try
            {
                var creator = _service.GetService<Employee>().GetAll().FirstOrDefault(e => e.EmployeeId == id);
                return creator != null ? $"คุณ {creator.Fullname}" : "ไม่ทราบผู้สร้าง";
            }
            catch (Exception ex) {
                return string.Empty;
            } 
        }

        public string getCreatorCustomer(int id)
        {
            try
            {
                var creator = _service.GetService<Customer>().GetAll().Where(e => e.CustomerId == id).FirstOrDefault();
                return creator != null ? $"คุณ {creator.Fullname}" : "ไม่ทราบผู้สร้าง";
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
    }
}
