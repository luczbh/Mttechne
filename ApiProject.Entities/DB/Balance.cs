using ApiProject.Infrastructure.Repository;


namespace ApiProject.Entities.DB
{
    public class Balance : DbEntity
    {
        public int BalanceId { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public DateTime Date { get; set; }
        public int? ClientId { get; set; }
        public int? ProductId { get; set; }
        public int? SellerId { get; set; }
        public Client? Client { get; set; }
        public Product? Product { get; set; }
        public Seller? Seller { get; set; }
    }
}
