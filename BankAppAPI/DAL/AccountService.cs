using BankApp.API.Models.DTO;
using BankApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.API.DAL
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAccount(AccountDTO accountDTO)
        {
            var newAccount = new Account()
            {
                FirstName = accountDTO.FirstName,
                LastName = accountDTO.LastName,
                AccountNumber = accountDTO.AccountNumber,
                Balance = accountDTO.InitialBalance,
                Transactions = new List<Models.Transaction>()
            };
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            //New transaction
            var InitialTransaction = new Models.Transaction
            {
                AccountId = newAccount.Id,
                Amount = accountDTO.InitialBalance,
                TransactionDate = DateTime.UtcNow,
                Description = "Initial Deposit",
                Type = TransactionType.Deposit
            };

            _context.Transactions.Add(InitialTransaction);
            await _context.SaveChangesAsync();

            return newAccount.Id;

        }

        public Task<Account> GetAccount(Guid id)
        {
            var account = _context.Accounts.Include(x =>
            x.Transactions).FirstOrDefaultAsync(a => a.Id == id);
            return account;
        }

        // GET: api/accounts
        public async Task<List<Account>> GetAllAccounts()
        {
           var accounts = await _context.Accounts
                          .OrderBy(c => c.FirstName)
                          .ToListAsync();
           return accounts;
        }

        //public async Task<IEnumerable<Account>> GetAllAccounts()
        //{
        //    try
        //    {
        //        var accounts = await _context.Accounts
        //                  .OrderBy(c => c.FirstName)
        //                  .ToListAsync();
        //        if (accounts == null)
        //        {
        //            return NotFound("No data Found..");
        //        }
        //        return Ok(accounts);
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
