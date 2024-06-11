using EventSourcingExample.Aggregates;
using EventSourcingExample.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace EventSourcingExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly BankAccountRepository _repository;

        public BankAccountController(BankAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            var accountId = Guid.NewGuid();
            var bankAccount = new BankAccount();
            bankAccount.CreateAccount(accountId, request.Owner);

            await _repository.SaveAsync(bankAccount);

            return Ok(new { AccountId = accountId });
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> DepositMoney(Guid accountId, [FromBody] DepositRequest request)
        {
            var bankAccount = await _repository.GetByIdAsync(accountId);
            bankAccount.DepositMoney(request.Amount);

            await _repository.SaveAsync(bankAccount);

            return Ok();
        }

        [HttpPost("{accountId}/withdraw")]
        public async Task<IActionResult> WithdrawMoney(Guid accountId, [FromBody] WithdrawRequest request)
        {
            var bankAccount = await _repository.GetByIdAsync(accountId);
            bankAccount.WithdrawMoney(request.Amount);

            await _repository.SaveAsync(bankAccount);

            return Ok();
        }

        [HttpGet("{accountId}/balance")]
        public async Task<IActionResult> GetBalance(Guid accountId)
        {
            try
            {
                var bankAccount = await _repository.GetByIdAsync(accountId);
                var balance = bankAccount.GetBalance();
                return Ok(new { AccountId = accountId, Balance = balance });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class CreateAccountRequest
    {
        public string Owner { get; set; }
    }

    public class DepositRequest
    {
        public decimal Amount { get; set; }
    }

    public class WithdrawRequest
    {
        public decimal Amount { get; set; }
    }
}
