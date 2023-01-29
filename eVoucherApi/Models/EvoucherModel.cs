using System.ComponentModel.DataAnnotations;

namespace eVoucherApi.Models
{
    public class EvoucherModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Image { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int Quantity { get; set; }
        public string BuyType { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int limit { get; set; }
    }

    public class EVoucherRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Image { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int Quantity { get; set; }
        public string BuyType { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int limit { get; set; }
        public string Token { get; set; }
        public int GiftPerLimit { get; set; }
    }
}
