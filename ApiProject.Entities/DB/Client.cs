using ApiProject.Infrastructure.Repository;

namespace ApiProject.Entities.DB
{
    public class Client : DbEntity
    {
        public int ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Operation> Operations { get; set; } = [];
        public List<Balance> Balances { get; set; } = [];
    }
}
