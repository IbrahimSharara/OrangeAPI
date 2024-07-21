using Microsoft.EntityFrameworkCore;
using OrangeTask.BLL.DTO.Output;
using OrangeTask.BLL.Interfaces;
using OrangeTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeTask.BLL.Services
{
    public class CustomerService : GeneralRepository<Customer>, ICustomerService
    {
        #region Injection
        private readonly OrangeTaskContext context;
        #endregion

        #region Constructor
        public CustomerService(OrangeTaskContext context) : base(context)
        {
            this.context = context;
        }
        #endregion

        #region Customers
        public async Task<CustomerPagination> Customers(int pageNumber  =1 , int pageSize= 20)
        {
            var customers = await GetAll().Include(s => s.Service).ToListAsync();
            CustomerPagination customerPagination = new CustomerPagination();

            customerPagination.customers = customers.Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .Select(X => new GetCustomersOutputDto { Name = X.FirstName + X.LastName, Email = X.Email, PhoneNumber = X.PhoneNumber, Service = X.Service.Name, CreatedAt = X.CreatedAt })
                                      .ToList();
            customerPagination.Count = customers.Count;
            customerPagination.Pages = customers.Count / pageSize;
            return customerPagination;
        }
        #endregion

        #region Get Customers  per year
        public async Task<List<CustomersPerMonthOutputDto>> CustomersPerYear(int year = 2024)
        {
            var customers = await context.Customers
            .GroupBy(c => c.CreatedAt.Month)
            .Select(g => new CustomersPerMonthOutputDto
            {
                MonthName = g.Key.ToString(),
                Count = g.Count()
            })
            .OrderBy(c => c.MonthName)
    .ToListAsync();
            return customers
            .Select(g => new CustomersPerMonthOutputDto
            {
                MonthName = new DateTime(year, int.Parse(g.MonthName), 1).ToString("MMMM"),
                Count = g.Count
            }).OrderBy(x=>x.MonthName)
            .ToList(); ;
        }
        #endregion

        #region Get Customers per month per year
        public async Task<List<CustomersOutputDto>> CustomersPerMonthPerYear(int year, int month)
        {
            var customers = await context.Customers.Where(x => x.CreatedAt.Year == year && x.CreatedAt.Month == month).
                Select(x => new CustomersOutputDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CreatedAt = x.CreatedAt,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Service = x.Service.Name
                }).ToListAsync();
            return customers;
        }
        #endregion

        #region Expired Contracts
        public async Task<List<CustomersOutputDto>> ExpiredCustomersContract()
        {
            var customers = await context.Customers.Where(x => DateTime.Now > x.Contract.EndDate).Select(X => new CustomersOutputDto
            {
                FirstName = X.FirstName,
                LastName = X.LastName,
                Email = X.Email,
                PhoneNumber = X.PhoneNumber,
                Service = X.Service.Name,
                CreatedAt = X.CreatedAt
            }).ToListAsync();
            return customers;
        }
        #endregion

        #region Expired contracts within month
        public async Task<List<CustomersOutputDto>> ExpiredCustomersContractwithinMonth()
        {
            var customers = await context.Customers.Where(x => x.Contract.EndDate.Month - DateTime.Now.Month == 1)
                .Select(x => new CustomersOutputDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CreatedAt = x.CreatedAt,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Service = x.Service.Name
                }).ToListAsync();
            return customers;
        }
        #endregion

        #region Customers per service
        public async Task<List<CustomersPerServiceOutputDto>> CustomersPerService()
        {
            var customers = await context.Customers
    .Join(
        context.Services,
        c => c.ServiceId,
        s => s.Id,
        (c, s) => new { c.ServiceId, s.Name }
    )
    .GroupBy(x => x.Name)
    .Select(g => new CustomersPerServiceOutputDto { ServiceName = g.Key, Count = g.Count() })
    .ToListAsync();
            return customers;
        }
        #endregion
    }
}
