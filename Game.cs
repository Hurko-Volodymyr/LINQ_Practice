namespace LINQ_Practice
{
    internal partial class Program
    {
        public class Game : IPlayable
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public decimal Price { get; set; }
            public DateTime ReleaseDate { get; set; }
            public int Rating { get; set; }
            public string Developer { get; set; }
            public bool IsMultiplayer { get; set; }
            public List<Review> Reviews { get; set; }
            public List<string> Tags { get; set; }
            public int SalesCount { get; set; }


        }
    }
}
