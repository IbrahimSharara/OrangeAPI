using Microsoft.EntityFrameworkCore;
using OrangeTask.BLL.DTO.Output;
using OrangeTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeTask.BLL.Interfaces
{
    public interface ICustomerService :IGeneralRepository<Customer>
    {
        public Task<CustomerPagination> Customers(int pageNumber, int pageSize);
        public Task<List<CustomersOutputDto>> ExpiredCustomersContract();
        public Task<List<CustomersOutputDto>> ExpiredCustomersContractwithinMonth();
        public Task<List<CustomersOutputDto>> CustomersPerMonthPerYear(int year , int month);
        public Task<List<CustomersPerMonthOutputDto>> CustomersPerYear(int year );
        public Task<List<CustomersPerServiceOutputDto>> CustomersPerService();
    }
}
