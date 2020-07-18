using System;
using System.Threading.Tasks;
using PaymentSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PaymentSystemAPI.Models.Requests;
using PaymentSystemAPI.Models.Responses;

namespace PaymentSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PaymentController : Controller
    {
        private IRepository repository;
        private ListErrors listErrors = new ListErrors();
        public PaymentController(IRepository repo)
        {
            this.repository = repo;
        }

        /// <summary>
        /// Метод, принимающий сумму и назначение платежа и возвращающий sessionId - идентификатор платёжной сессии, например, 4e273d86-864c-429d-879a-34579708dd69.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> CreatePayment([FromBody] PaymentInfoFromRequest info)
        {
            #region Verify

            listErrors.Errors = new List<Error>();

            if (info.Amount < 0)
            {
                listErrors.Errors.Add(new Error() { Status = "400", Title = "Negative amount" });
                return BadRequest(listErrors);
            }
            #endregion

            PaymentInfo payInfo = new PaymentInfo()
            {
                SessionId = Guid.NewGuid().ToString(),
                Amount = info.Amount,
                ArrivalTime = DateTime.Now,
                Description = info.Description
            };

            await repository.SavePaymentInfoAsync(payInfo);

            return Ok(new Session() { SessionId = payInfo.SessionId });
        }

    }
}