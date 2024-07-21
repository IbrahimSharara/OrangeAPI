using OrangeTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrangeTask.DAL.DataSeeding
{
    public class SeedingData
    {
        public static async Task SeedAsync(OrangeTaskContext context)
        {
            if (!context.Services.Any())
            {
                var Services = File.ReadAllText("../OrangeTask.DAL/DataSeeding/Service.json");
                List<Service> ServicesData = JsonSerializer.Deserialize<List<Service>>(Services);
                context.Services.AddRangeAsync(ServicesData);
                await context.SaveChangesAsync();
            }
            if (!context.Customers.Any())
            {
                var Customers = File.ReadAllText("../OrangeTask.DAL/DataSeeding/Customer.json");
                List<Customer> customersData = JsonSerializer.Deserialize<List<Customer>>(Customers);
                context.Customers.AddRangeAsync(customersData);
                await context.SaveChangesAsync();
            }
            if (!context.Contracts.Any())
            {
                var Contracts = File.ReadAllText("../OrangeTask.DAL/DataSeeding/Contract.json");
                List<Contract> ContractsData = JsonSerializer.Deserialize<List<Contract>>(Contracts);
                context.Contracts.AddRangeAsync(ContractsData);
                await context.SaveChangesAsync();
            }
            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
