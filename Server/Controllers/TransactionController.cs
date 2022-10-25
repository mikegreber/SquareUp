using Microsoft.AspNetCore.Mvc;
using SquareUp.Server.Services.Transactions;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.ControllerEndpoints.Transactions;

namespace SquareUp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<TransactionBase>>>> GetTransactions()
        {
            var result = await _service.GetAllTransactions();
            return Ok(result);
        }

        [HttpPost(PostAddTransactionUri)]
        public async Task<ActionResult<ServiceResponse<TransactionBase>>> Create(TransactionRequest request)
        {
            var result = await _service.Create(Request, request);
            return Ok(result);
        }

        [HttpPut(PutEditTransactionUri)]
        public async Task<ActionResult<ServiceResponse<TransactionBase>>> Update(TransactionBase request)
        {
            var result = await _service.Update(Request, request);
            return Ok(result);
        }

        [HttpDelete(DeleteTransactionUri)]
        public async Task<ActionResult<ServiceResponse<TransactionBase>>> Delete(int transactionId)
        {
            var result = await _service.Delete(Request, transactionId);
            return Ok(result);
        }
    }
}
