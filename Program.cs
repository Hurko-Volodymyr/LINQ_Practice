using System.Diagnostics.Metrics;
using static LINQ_Practice.Program;
using static System.Formats.Asn1.AsnWriter;

namespace LINQ_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var games = GetSampleData();

            // Easy
            // Task 1. Basic Filtering (Where). Find all games with a rating of 8 or higher

            var highRatedGames = games.Where(g => g.Rating >= 8);

            foreach (var game in highRatedGames)
            {
                Console.WriteLine(game.Title);
            }

            // Task 2. Ordering (OrderBy). Get all games ordered by price(lowest first)

            var orderedByPrice = games.OrderBy(g => g.Price);

            foreach (var game in orderedByPrice)
            {
                Console.WriteLine(game.Title);
            }

            // Task 3. Multiple Ordering (OrderBy, ThenBy).
            // Get all games ordered by genre, then by rating(highest first)
            var orderedGames = games.OrderBy(g => g.Genre).ThenByDescending(g => g.Rating);

            foreach (var game in orderedGames)
            {
                Console.WriteLine(game.Title);
            }

            // Task 4. Find all multiplayer games under $40 with rating above 7 (Where)
            var filteredGames = games.Where(g => g.IsMultiplayer && g.Price < 40 && g.Rating > 7);

            foreach (var game in filteredGames)
            {
                Console.WriteLine(game.Title);
            }

            // Task 5. First Element (First/FirstOrDefault). Get the oldest game in the collection
            var oldestGame = games.OrderBy(g => g.ReleaseDate).First();
            Console.WriteLine(oldestGame.Title);

            // Task 6. Last Element. Get the most recently released game
            var newestGame = games.OrderBy(g => g.ReleaseDate).LastOrDefault();
            Console.WriteLine(newestGame.Title);

            // Task 7. Single Element (SingleOrDefault). Find the game titled "Minecraft"
            var minecraft = games.SingleOrDefault(g => g.Title == "Minecraft");
            Console.WriteLine(minecraft.Title);

            // Task 8. Count how many games are in the RPG genre
            var rpgCount = games.Count(g => g.Genre == "RPG");
            Console.WriteLine(rpgCount);

            // Middle
            // Task 9. Find the lowest price among all games and print Titles (2 Queries)
            var lowestPrice = games.Min(g => g.Price);
            var lowestPriceGameTitles = games.Where(g => g.Price == lowestPrice);

            foreach (var game in lowestPriceGameTitles)
            {
                Console.WriteLine($"Lowest price: {game.Price}, Title: {game.Title}");
            }

            // Task 10. Calculate the average rating of all games
            var avgRating = games.Average(g => g.Rating);
            Console.WriteLine(avgRating);

            // Task 11. Check if there are any games developed by "Valve"
            var hasValveGames = games.Any(g => g.Developer == "Valve");
            Console.WriteLine(hasValveGames);

            // Task 12. Check if all games have at least one review
            var allHaveReviews = games.All(g => g.Reviews.Any());
            Console.WriteLine(allHaveReviews);

            // Task 13. Get a list ONLY of all game titles and their release years begginning with the newest 
            var titleYears = games.Select(g => new { g.Title, Year = g.ReleaseDate.Year }).OrderByDescending(g => g.Year);

            foreach (var game in titleYears)
            {
                Console.WriteLine($"Year: {game.Year}, Title: {game.Title}");
            }

            // Hard
            // Task 14. Get games with their price-to-rating ratio
            var pricePerRating = games.Select(g => new {
                g.Title,
                PricePerRating = g.Price / g.Rating
            });

            foreach (var game in pricePerRating)
            {
                Console.WriteLine($"Rating: {game.PricePerRating}, Title: {game.Title}");
            }

            // Task 15. Get all review scores across all games of CD Projekt Red
            var allScoresByDeveloper = games
             .Where(g => g.Developer == "CD Projekt Red")
             .SelectMany(g => g.Reviews.Select(r => new { g.Developer, r.Score }));

            foreach (var item in allScoresByDeveloper)
            {
                Console.WriteLine($"Developer: {item.Developer}, Score: {item.Score}");
            }

            // Task 16. Find the average review score for games released after 2020
            var avgNewGameScore = games
                .Where(g => g.ReleaseDate.Year > 2020)
                .SelectMany(g => g.Reviews)
                .Average(r => r.Score);
            Console.WriteLine(avgNewGameScore);

            // Task 17. Count games by genre
            var gamesByGenre = games
                .GroupBy(g => g.Genre)
                .Select(g => new { Genre = g.Key, Count = g.Count() });

            foreach (var game in gamesByGenre)
            {
                Console.WriteLine($"Genre: {game.Genre}, Count: {game.Count}");
            }

            // Task 18. Find games that have all verified purchase reviews
            var allVerifiedGames = games
            .Where(g => g.Reviews.All(r => r.IsVerifiedPurchase));

            foreach (var game in allVerifiedGames)
            {
                Console.WriteLine($"Game: {game.Title}");
            }

            // Task 19. Find all unique tags across all games
            var uniqueTags = games.SelectMany(g => g.Tags)
                .Distinct()
                .OrderBy(t => t);

            foreach (var tag in uniqueTags)
            {
                Console.WriteLine($"Tag: {tag}");
            }

            // Task 20. Find the top 3 games by sales count with their developer
            var topSellers = games.OrderByDescending(g => g.SalesCount)
                .Take(3)
                .Select(g => new { g.Title, g.Developer, g.SalesCount });

            foreach (var game in topSellers)
            {
                Console.WriteLine($"Game: {game.Title}, Developer: {game.Developer}, Sales count: {game.SalesCount}");
            }

        }

        private static List<Game> GetSampleData()
        {
            return new List<Game>    {
        new Game
        {
            Id = 1,
            Title = "The Witcher 3",
            Genre = "RPG",
            Price = 39.99m,
            ReleaseDate = new DateTime(2015, 5, 19),
            Rating = 9,
            Developer = "CD Projekt Red",
            IsMultiplayer = false,
            SalesCount = 50000000,
            Tags = new List<string> { "Fantasy", "Open World", "Action RPG" },
            Reviews = new List<Review>
            {
                new Review { Id = 1, Score = 10, PostDate = new DateTime(2015, 5, 20), UserName = "GameLover", Text = "Masterpiece!", IsVerifiedPurchase = true },
                new Review { Id = 2, Score = 9, PostDate = new DateTime(2015, 5, 21), UserName = "RPGFan", Text = "Amazing game", IsVerifiedPurchase = true }
            }
        },
        new Game
        {
            Id = 2,
            Title = "Cyberpunk 2077",
            Genre = "RPG",
            Price = 59.99m,
            ReleaseDate = new DateTime(2020, 12, 10),
            Rating = 7,
            Developer = "CD Projekt Red",
            IsMultiplayer = false,
            SalesCount = 20000000,
            Tags = new List<string> { "Sci-Fi", "Open World", "FPS" },
            Reviews = new List<Review>
            {
                new Review { Id = 3, Score = 6, PostDate = new DateTime(2020, 12, 11), UserName = "CyberFan", Text = "Buggy launch", IsVerifiedPurchase = true },
                new Review { Id = 4, Score = 8, PostDate = new DateTime(2021, 1, 15), UserName = "NightCity", Text = "Much better after patches", IsVerifiedPurchase = true }
            }
        },
        new Game
        {
            Id = 3,
            Title = "Counter-Strike 2",
            Genre = "FPS",
            Price = 0m,
            ReleaseDate = new DateTime(2023, 9, 27),
            Rating = 8,
            Developer = "Valve",
            IsMultiplayer = true,
            SalesCount = 75000000,
            Tags = new List<string> { "Shooter", "Competitive", "Multiplayer" },
            Reviews = new List<Review>
            {
                new Review { Id = 5, Score = 9, PostDate = new DateTime(2023, 9, 28), UserName = "ProGamer", Text = "Great upgrade", IsVerifiedPurchase = true },
                new Review { Id = 6, Score = 7, PostDate = new DateTime(2023, 9, 29), UserName = "CSVeteran", Text = "Needs optimization", IsVerifiedPurchase = true }
            }
        },
        new Game
        {
            Id = 4,
            Title = "Minecraft",
            Genre = "Sandbox",
            Price = 29.99m,
            ReleaseDate = new DateTime(2011, 11, 18),
            Rating = 10,
            Developer = "Mojang",
            IsMultiplayer = true,
            SalesCount = 300000000,
            Tags = new List<string> { "Crafting", "Survival", "Building" },
            Reviews = new List<Review>
            {
                new Review { Id = 7, Score = 10, PostDate = new DateTime(2011, 11, 19), UserName = "BlockMaster", Text = "Revolutionary!", IsVerifiedPurchase = true },
                new Review { Id = 8, Score = 10, PostDate = new DateTime(2011, 11, 20), UserName = "CreativeMind", Text = "Endless possibilities", IsVerifiedPurchase = true }
            }
        },
        new Game
        {
            Id = 5,
            Title = "Red Dead Redemption 2",
            Genre = "Action-Adventure",
            Price = 59.99m,
            ReleaseDate = new DateTime(2018, 10, 26),
            Rating = 10,
            Developer = "Rockstar Games",
            IsMultiplayer = true,
            SalesCount = 55000000,
            Tags = new List<string> { "Western", "Open World", "Story Rich" },
            Reviews = new List<Review>
            {
                new Review { Id = 9, Score = 10, PostDate = new DateTime(2018, 10, 27), UserName = "Cowboy", Text = "Best game ever!", IsVerifiedPurchase = true },
                new Review { Id = 10, Score = 9, PostDate = new DateTime(2018, 10, 28), UserName = "OutlawLife", Text = "Incredible detail", IsVerifiedPurchase = false }
            }
        }
            };
        }

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

    public interface IPlayable
    {
        void Play()
        {
            Console.WriteLine("Game started...");
        }
    }
}
