using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrangeTask.BLL.Interfaces;
using OrangeTask.BLL.Services;
using OrangeTask.DAL.Entities;

namespace OrangeTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService repository;

        public CustomerController(ICustomerService repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers(int pageNumber, int pageSize)
        {
            var customers = await repository.Customers(pageNumber , pageSize);
            return Ok(customers);
        }
        
        [HttpGet("ExpiredContracts")]
        public async Task<IActionResult> ExpiredContracts()
        {
            var customers = await repository.ExpiredCustomersContract();
            return Ok(customers);
        }

        [HttpGet("CustomersPerMonth")]
        public async Task<IActionResult> CustomersPerMonth(int year , int month)
        {
            var customers = await repository.CustomersPerMonthPerYear(year , month);
            return Ok(customers);
        }
        
        [HttpGet("CustomersPerYear")]
        public async Task<IActionResult> CustomersPerYear(int year )
        {
            var customers = await repository.CustomersPerYear(year);
            return Ok(customers);
        }
        [HttpGet("ExpirewithinMonth")]
        public async Task<IActionResult> ExpiredCustomersContractwithinMonth()
        {
            var customers = await repository.ExpiredCustomersContractwithinMonth();
            return Ok(customers);
        }
        [HttpGet("CustomersPerService")]
        public async Task<IActionResult> CustomersPerService()
        {
            var customers = await repository.CustomersPerService();
            return Ok(customers);
        }
    }
}
