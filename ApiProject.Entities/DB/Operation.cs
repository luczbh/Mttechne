using ApiProject.Entities.Enumerators;
using ApiProject.Infrastructure.Repository;

namespace ApiProject.Entities.DB
{
    public class Operation : DbEntity
    {
        public int OperationId { get; set; }
        public int? ClientId { get; set; }
        public int ProductId { get; set; }
        public int SellerId { get; set; }
        public Decimal Value { get; set; }
        public DateTime OperationDate { get; set; }
        public EOperationType OperationType { get; set; }
        public Client? Client { get; set; }
        public Product Product { get; set; }
        public Seller Seller { get; set; }
    }
}

