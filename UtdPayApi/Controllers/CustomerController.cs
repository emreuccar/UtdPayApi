using FunTranslator.Data.Models;
using Microsoft.AspNetCore.Mvc;
using UtdPayApi.Business.Identity;
using UtdPayApi.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UtdPayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly PaymentDbContext _context;

        public CustomerController(ILogger<CustomerController> logger, PaymentDbContext paymentDbContext)
        {
            _logger = logger;
            _context = paymentDbContext;
        }

        [HttpPost]
        public void Create(CustomerRequestModel customerRequestModel)
        {
            var dataService = new DataService(_context);
            var customerId = dataService.CreateCustomer(customerRequestModel);

            var identityNoVerified = new KPSService().ValidateIdentity(customerRequestModel);

            dataService.UpdateCustomer(customerId, identityNoVerified);
        }
    }
}