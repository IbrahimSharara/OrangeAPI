using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeTask.BLL.DTO.Output
{
    public class CustomerPagination
    {
        public List<GetCustomersOutputDto> customers { get; set; }
        public int Pages { get; set; }
        public int Count { get; set; }
    }
}
