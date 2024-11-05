namespace ApiProject.Entities.Models
{
    public class BalanceModel
    {
        public DateTime Date { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public string Description { get; set; } = string.Empty;
        public IEnumerable<BalanceItemModel> Operations { get; set; }
    }
}
