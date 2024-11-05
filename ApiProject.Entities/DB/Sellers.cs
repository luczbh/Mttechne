using ApiProject.Infrastructure.Repository;

namespace ApiProject.Entities.DB
{
    public class Seller : DbEntity
    {
        public int SellerId { get; set; }
        public required string SellerName { get; set; }
        public List<Operation> Operations { get; set; } = [];
        public List<Balance> Balances { get; set; } = [];
    }
}
