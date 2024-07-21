using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeTask.DAL.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int ServiceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Service Service { get; set; }
        public Contract Contract { get; set; }
    }
}
