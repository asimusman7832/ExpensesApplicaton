namespace ExpensesAPI.DTOs
{
    public class PostTransactionDTO
    {
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
    }
}
