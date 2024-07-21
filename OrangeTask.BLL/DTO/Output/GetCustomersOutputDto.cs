using OrangeTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeTask.BLL.DTO.Output
{
    public class GetCustomersOutputDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Service { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
