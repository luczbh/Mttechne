using ApiProject.Entities.Enumerators;

namespace ApiProject.Entities.Models
{
    public class OperationModel
    {
        public EOperationType OperationType { get; set; }
        public int? ClientId { get; set; }
        public int ProductId { get; set; }
        public int SellerId { get; set; }
        public decimal Value { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
