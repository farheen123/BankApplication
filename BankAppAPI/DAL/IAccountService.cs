using BankApp.API.Models.DTO;
using BankApp.API.Models;

namespace BankApp.API.DAL
{
    public interface IAccountService
    {
        Task<Guid> CreateAccount(AccountDTO accountDTO);
        Task<Account> GetAccount(Guid id);
        Task<List<Account>> GetAllAccounts();

    }
}
