using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentSystemAPI.Models;
using PaymentSystemAPI.Models.Requests;
using PaymentSystemAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CardController : Controller
    {
        private IRepository repository;
        private ListErrors listErrors = new ListErrors();
        private Settings settings;
        public CardController(IRepository repo, IOptions<Settings> set)
        {
            this.repository = repo;
            this.settings = set.Value;
        }

        /// <summary>
        /// Метод, который принимает данные карты и sessionId, проверяет номер карты по алгоритму Луна
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> CreateCardAsync([FromBody] CardInfoFromRequest info)
        {
            DateTime cardDate = new DateTime();

            #region Verify

            listErrors.Errors = new List<Error>();

            //  Проверка номера карты по алгоритму Луна
            if (!algoritmLuhn(info.CardNumber))
                listErrors.Errors.Add(new Error() { Status = "400", Title = "Invalid card number" });

            //  Проверка срока действия карты
            try
            {
                int[] monthAndYear = info.CardDate.Split("/").Select(str => int.Parse(str)).ToArray();
                if (monthAndYear.Length != 2)
                    throw new Exception();
                monthAndYear[1] += 2000;
                string date = string.Format("{0}/{1} 23:59:59",
                    DateTime.DaysInMonth(monthAndYear[1], monthAndYear[0]),
                    monthAndYear[0].ToString() + "/" + monthAndYear[1]);
                if (!DateTime.TryParse(date, out cardDate))
                    throw new Exception();
            }
            catch (Exception)
            {
                listErrors.Errors.Add(new Error() { Status = "400", Title = "Invalid card date" });
            }

            //  Проверка на существование платежа
            if (!repository.PaymentInfo.Any(pay => pay.SessionId == info.SessionId))
            {
                listErrors.Errors.Add(new Error() { Status = "400", Title = "Invalid sessionId" });
                return BadRequest(listErrors);
            }

            //  Не прошло ли время жизни платёжной сессии
            PaymentInfo paymentInfo = repository.PaymentInfo.First(s => s.SessionId == info.SessionId);
            if (DateTime.Now - paymentInfo.ArrivalTime > TimeSpan.FromSeconds(settings.LifeTimeSession))
                listErrors.Errors.Add(new Error() { Status = "400", Title = "The payment session has expired" });

            if (listErrors.Errors.Count != 0)
                return BadRequest(listErrors);

            #endregion

            CardInfo cardInfo = new CardInfo()
            {
                CardNumber = info.CardNumber,
                CardDate = cardDate,
                VerificationNumber = info.VerificationCode,
                PaymentInfo = paymentInfo
            };
            await repository.SaveCardInfoAsync(cardInfo);

            Receipt receipt = new Receipt()
            {
                SessionId = paymentInfo.SessionId,
                Amount = paymentInfo.Amount,
                Description = paymentInfo.Description,
                ArrivalTime = paymentInfo.ArrivalTime,
                CardNumber = info.CardNumber,
                TimePayment = DateTime.Now
            };
            await repository.SaveReceiptAsync(receipt);

            return Ok(receipt);
        }


        /// <summary>
        /// Проверка номера карты по алгоритму Луна (упрощенному)
        /// </summary>
        private static bool algoritmLuhn(string creditCardNumber)
        {
            int sumOfDigits = creditCardNumber.Select(e => (int)(e - '0'))
                            .Reverse()
                            .Select((e, i) => e * (i % 2 == 0 ? 1 : 2))
                            .Sum(e => e < 10 ? e : e -= 9);

            return sumOfDigits % 10 == 0;
        }
    }
}