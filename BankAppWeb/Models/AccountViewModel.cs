using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Web.Models
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        //public List<Transaction> Transactions { get; set; }
    }
}
