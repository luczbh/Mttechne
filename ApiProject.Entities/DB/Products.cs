using ApiProject.Infrastructure.Repository;

namespace ApiProject.Entities.DB
{
    public class Product : DbEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public List<Operation> Operations { get; set; } = [];
        public List<Balance> Balances { get; set; } = [];
    }
}
