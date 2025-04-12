using System.ComponentModel.DataAnnotations;

namespace MarketingSolutions.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime? ProductAddingDate { get; set; } = DateTime.Now;

        // Foreign Key for Company
        public string UserId { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public double PackSize {  get; set; }
        [Required]
        public double Quantity { get; set; } = 0;
        public double SoldQuantity { get; set; } = 0;
        [Required]
        public double ActualPrice { get; set; }
        public double TotalActualPrice => Quantity * ActualPrice == 0 ? 0 : Quantity * ActualPrice;
        [Required]
        public double SellPrice { get; set; }
        public double TotalSellPrice => Quantity * SellPrice == 0 ? 0 : Quantity * SellPrice;

        public double ProfitOrLoss => (SoldQuantity * SellPrice) - (SoldQuantity * ActualPrice);
    }
}
