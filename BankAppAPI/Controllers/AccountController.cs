using BankApp.API.DAL;
using BankApp.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<Guid> CreateAccount(AccountDTO accountDTO)
        {
            var accountId = await _accountService.CreateAccount(accountDTO);
            return accountId;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _accountService.GetAllAccounts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var account = await _accountService.GetAccount(id);
                if (account == null)
                {
                    return NotFound($"Account not found with id {id}");
                }
                return Ok(account);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
