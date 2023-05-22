using FunTranslator.Data.Models;
using Microsoft.EntityFrameworkCore;
using UtdPayApi.Data.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UtdPayApi.Data
{
    public class DataService
    {
        private readonly PaymentDbContext _context;

        public DataService(PaymentDbContext paymentDbContext)
        {
            _context = paymentDbContext;
        }

        public long CreateCustomer(CustomerRequestModel customerRequestModel)
        {
            var customer = new Customer
            {
                Name = customerRequestModel.Name,
                Surname = customerRequestModel.Surname,
                BirthDate = customerRequestModel.BirthDate.Date,
                IdentityNo = customerRequestModel.IdentityNo,
                IdentityNoVerified = false,
                Status = "P"
            };

            _context!.Customers!.Add(customer);
            _context.SaveChanges();

            return customer.CustomerId;
        }

        public void UpdateCustomer(long customerId, bool identityNoVerified)
        {
            var customer = _context?.Customers!
             .FirstOrDefault(t => t.CustomerId == customerId);

            if (customer != null)
            {
                customer.IdentityNoVerified = identityNoVerified;
                customer.Status = identityNoVerified ? "A" : "P";

                _context.SaveChanges();
            }         
        }

        public void SavePayment(Payment payment)
        {
            _context!.Payments!.Add(payment);
            _context.SaveChanges();
        }
    }
}
