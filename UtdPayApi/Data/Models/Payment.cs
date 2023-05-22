using System.ComponentModel.DataAnnotations;

namespace UtdPayApi.Data.Models
{
    public class Payment
    {
        [Key]
        public Guid TransactionId { get; set; }
        public string CustomerId { get; set; }
        public string OrderId { get; set; }
        public string TxnType { get; set; }
        public string TotalAmount { get; set; }
        public string CardNumber { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string StatusCode { get; set; }
    }

    public class PaymentRequestModel
    {
        public string MerchantId { get; set; }
        public int MemberId { get; set; }
        public string CustomerId { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDateMonth { get; set; }
        public string ExpiryDateYear { get; set; }
        public string Cvv { get; set; }
        public string UserCode { get; set; }
        public string TxnType { get; set; }
        public int InstallmentCount { get; set; }
        public string Currency { get; set; }
        public string OrderId { get; set; }
        public string TotalAmount { get; set; }
        public string Rnd { get; set; }
        public string Hash { get; set; }
        public string Amount { get; set; }
    }

    public class PaymentResponseModel
    {
        public bool fail { get; set; }
        public int statusCode { get; set; }
        public Result result { get; set; }
        public int count { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
    }

    public class Result
    {
        public object url { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public object bankResponseMessage { get; set; }
        public string orderId { get; set; }
        public object bankOrderNo { get; set; }
        public object txnType { get; set; }
        public object txnStatus { get; set; }
        public int vposId { get; set; }
        public object vposName { get; set; }
        public object authCode { get; set; }
        public object hostReference { get; set; }
        public object totalAmount { get; set; }
        public bool hideResponseTarget { get; set; }
        public int saleDate { get; set; }
        public object paymentSystem { get; set; }
        public object responseHash { get; set; }
        public object installmentCount { get; set; }
    }

}
