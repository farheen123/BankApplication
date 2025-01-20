namespace BankApp.API.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance  { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
