namespace LINQ_Practice
{
    internal partial class Program
    {
        public class Review
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public int Score { get; set; }
            public DateTime PostDate { get; set; }
            public string UserName { get; set; }
            public bool IsVerifiedPurchase { get; set; }
        }
    }
}
