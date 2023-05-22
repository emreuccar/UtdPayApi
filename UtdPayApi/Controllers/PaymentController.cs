using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using UtdPayApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using UtdPayApi.Data;
using FunTranslator.Data.Models;

namespace UtdPayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly PaymentDbContext _context;

        public PaymentController(ILogger<PaymentController> logger, PaymentDbContext paymentDbContext)
        {
            _logger = logger;
            _context = paymentDbContext;
        }

        [HttpPost]
        public void Sale(long customerId)
        {
            var token = Login();

            if (string.IsNullOrEmpty(token))
                return;

            Pay(token, customerId);
        }

        private string Login()
        {
            try
            {
                var loginRequestModel = new LoginRequestModel
                {
                    Email = "murat.karayilan@dotto.com.tr",
                    ApiKey = "kU8@iP3@",
                    Lang = "TR"
                };

                using (var client = new HttpClient())
                {
                    var uri = "https://ppgsecurity-test.birlesikodeme.com:55002/api/ppg/Securities/authenticationMerchant";
                    var postTask = client.PostAsJsonAsync(uri, loginRequestModel);
                    postTask.Wait();

                    var result = postTask.Result;

                    var response = result.Content.ReadAsStringAsync().Result;

                    var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(response);

                    return loginResponse!.result.token;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void Pay(string token, long customerId)
        {
            try
            {
                var paymentRequestModel = new PaymentRequestModel
                {
                    MerchantId = "1894",
                    MemberId = 1,
                    CustomerId = customerId.ToString(),
                    CardNumber = "5101385101385104",
                    ExpiryDateMonth = "12",
                    ExpiryDateYear = "22",
                    Cvv = "000",
                    UserCode = "test",
                    TxnType = "Auth",
                    InstallmentCount = 1,
                    Currency = "949",
                    OrderId = "1",
                    TotalAmount = "1",
                    Rnd = "abcd",
                    Amount = "1"
                };

                paymentRequestModel.Hash = CreateHash(paymentRequestModel);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);

                    var uri = "https://ppgpayment-test.birlesikodeme.com:20000/api/ppg/Payment/NoneSecurePayment";
                    var postTask = client.PostAsJsonAsync(uri, paymentRequestModel);
                    postTask.Wait();

                    var result = postTask.Result;

                    var response = result.Content.ReadAsStringAsync().Result;

                    var paymentResponse = JsonConvert.DeserializeObject<PaymentResponseModel>(response);

                    SavePayment(paymentRequestModel, paymentResponse);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string CreateHash(PaymentRequestModel request)
        {
            var apiKey = "SKI0NDHEUP60J7QVCFATP9TJFT2OQFSO";

            var hashString = $"{apiKey}{request.UserCode}{request.Rnd}{request.TxnType}{request.TotalAmount}{request.CustomerId}{request.OrderId}";

            System.Security.Cryptography.SHA512 s512 = System.Security.Cryptography.SHA512.Create();

            System.Text.UnicodeEncoding ByteConverter = new System.Text.UnicodeEncoding();

            byte[] bytes = s512.ComputeHash(ByteConverter.GetBytes(hashString));

            var hash = System.BitConverter.ToString(bytes).Replace("-", "");

            return hash;

        }

        private void SavePayment(PaymentRequestModel request, PaymentResponseModel response)
        {
            if (response == null)
                return;

            Payment payment = new Payment
            {
                ResponseMessage = response.responseMessage,
                ResponseCode = response.responseCode,
                CardNumber = request.CardNumber,
                CustomerId = request.CustomerId,
                OrderId = request.OrderId,
                StatusCode = response.statusCode.ToString(),
                TotalAmount = request.TotalAmount,
                TransactionId = Guid.NewGuid(),
                TxnType = request.TxnType
            };

            var dataService = new DataService(_context);
            dataService.SavePayment(payment);
        }
    }
}