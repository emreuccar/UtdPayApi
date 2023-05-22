using System.ComponentModel.DataAnnotations;

namespace FunTranslator.Data.Models
{
    public class Customer
    {
        [Key]
        public long CustomerId { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public long IdentityNo { get; set; } 
        public bool IdentityNoVerified { get; set; } = false;
        public string Status { get; set; } = default!;
    }

    public class CustomerRequestModel
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public long IdentityNo { get; set; }
    }
}