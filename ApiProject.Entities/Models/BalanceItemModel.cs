namespace ApiProject.Entities.Models
{
    public class BalanceItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Credits { get; set; }
        public decimal Debits { get; set; }
    }

}
