using System;
using System.Linq;
using System.Threading.Tasks;
using APIPaymentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using APIPaymentSystem.Models.Requests;
using APIPaymentSystem.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace APIPaymentSystem.Controllers
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
                SessionId = Guid.NewGuid(),
                Amount = info.Amount,
                ArrivalTime = DateTime.Now,
                Description = info.Description
            };

            await repository.SavePaymentInfoAsync(payInfo);

            return Ok(new Session() { SessionId = payInfo.SessionId });
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetPayments(DateTime from, DateTime to)
        {
            return Ok(repository.Payments.Where(payment => payment.ArrivalTime > from && payment.ArrivalTime < to));
        }
    }
}