using System.Diagnostics.Metrics;
using static LINQ_Practice.Program;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LINQ_Practice
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            var games = GetSampleData();

            // Easy
            // Task 1. Basic Filtering (Where). Find all games with a rating of 8 or higher

            Console.WriteLine("Task 1. Find all games with a rating of 8 or higher");
            Console.WriteLine("==================================");

            var highRatedGames = games.Where(g => g.Rating >= 8);

            foreach (var game in highRatedGames)
            {
                Console.WriteLine($"Game with a rating of 8 or higher: {game.Title}");
            }
            Console.WriteLine("==================================");


            // Task 2. Ordering (OrderBy). Get all games ordered by price(lowest first)

            Console.WriteLine("Task 2. Get all games ordered by price(lowest first)");
            Console.WriteLine("==================================");

            var orderedByPrice = games.OrderBy(g => g.Price);

            foreach (var game in orderedByPrice)
            {
                Console.WriteLine($"Game: {game.Title}, Price: {game.Price}");
            }
            Console.WriteLine("==================================");


            // Task 3. Multiple Ordering (OrderBy, ThenBy).
            // Get all games ordered by genre, then by rating(highest first)

            Console.WriteLine("Task 3. Get all games ordered by genre, then by rating(highest first)");
            Console.WriteLine("==================================");

            var orderedGames = games.OrderBy(g => g.Genre).ThenByDescending(g => g.Rating);

            foreach (var game in orderedGames)
            {
                Console.WriteLine($"Game: {game.Title}, Genre: {game.Genre}, Rating: {game.Rating}");
            }
            Console.WriteLine("==================================");

            // Task 4. Find all multiplayer games under $40 with rating above 7 (Where)

            Console.WriteLine("Task 4. Find all multiplayer games under $40 with rating above 7");
            Console.WriteLine("==================================");

            var filteredGames = games.Where(g => g.IsMultiplayer && g.Price < 40 && g.Rating > 7);

            foreach (var game in filteredGames)
            {
                Console.WriteLine($"Game: {game.Title}, Price: {game.Price}, Rating: {game.Rating}");
            }
            Console.WriteLine("==================================");

            // Task 5. First Element (First/FirstOrDefault). Get the oldest game in the collection

            Console.WriteLine("Task 5. Get the oldest game in the collection");
            Console.WriteLine("==================================");

            var oldestGame = games.OrderBy(g => g.ReleaseDate).First();
            Console.WriteLine($"Game: {oldestGame.Title}, Release date: {oldestGame.ReleaseDate}");
            Console.WriteLine("==================================");

            // Task 6. Last Element. Get the most recently released game

            Console.WriteLine("Task 6. Get the most recently released game (Last)");
            Console.WriteLine("==================================");

            var newestGame = games.OrderBy(g => g.ReleaseDate).LastOrDefault();
            Console.WriteLine($"Game: {newestGame.Title}, Release date: {newestGame.ReleaseDate}");
            Console.WriteLine("==================================");

            // Task 7. Single Element (SingleOrDefault). Find the game titled "Minecraft"

            Console.WriteLine("Task 7. Find the game titled \"Minecraft\"");
            Console.WriteLine("==================================");
            var minecraft = games.SingleOrDefault(g => g.Title == "Minecraft");
            Console.WriteLine(minecraft.Title);
            Console.WriteLine("==================================");

            // Task 8. Count how many games are in the RPG genre

            Console.WriteLine("Task 8. Count how many games are in the RPG genre");
            Console.WriteLine("==================================");
            var rpgCount = games.Count(g => g.Genre == "RPG");
            Console.WriteLine($"Count of games are in the RPG genre: {rpgCount}");
            Console.WriteLine("==================================");

            // Middle
            // Task 9. Find the lowest price among all games and print Title and Price of that games

            Console.WriteLine("Task 9. Find the lowest price among all games and print Title and Price of that games");
            Console.WriteLine("==================================");

            var lowestPriceGames = games
                .Where(g => g.Price == games.Min(x => x.Price))
                .Select(g => new { g.Title, g.Price });

            foreach (var game in lowestPriceGames)
            {
                Console.WriteLine($"Game {game.Title} with the lowest price: {game.Price}");
            }

            Console.WriteLine("==================================");

            // Task 10. Calculate the average rating of all games

            Console.WriteLine("Task 10. Calculate the average rating of all games");
            Console.WriteLine("==================================");
            var avgRating = games.Average(g => g.Rating);
            Console.WriteLine($"The average rating of all games: {avgRating}");
            Console.WriteLine("==================================");

            // Task 11. Check if there are any games developed by "Valve"

            Console.WriteLine("Task 11. Check if there are any games developed by \"Valve\"");
            Console.WriteLine("==================================");

            var hasValveGames = games.Any(g => g.Developer == "Valve");
            Console.WriteLine($"Are there any games developed by \"Valve\"? {hasValveGames}");
            Console.WriteLine("==================================");

            // Task 12. Check if all games have at least one review

            Console.WriteLine("Task 12. Check if all games have at least one review");
            Console.WriteLine("==================================");

            var allHaveReviews = games.All(g => g.Reviews.Any());
            Console.WriteLine($"Are there all games have at least one review? {allHaveReviews}");
            Console.WriteLine("==================================");

            // Hard
            // Task 13. Get a list ONLY of all game titles and their release years begginning with the newest 

            Console.WriteLine("Task 13. Get a list ONLY of all game titles and their release years begginning with the newest");
            Console.WriteLine("==================================");

            var titleYears = games.Select(g => new { g.Title, Year = g.ReleaseDate.Year }).OrderByDescending(g => g.Year);

            foreach (var game in titleYears)
            {
                Console.WriteLine($"Year: {game.Year}, Title: {game.Title}");
            }
            Console.WriteLine("==================================");


            // Task 14. Get games with their price-to-rating ratio

            Console.WriteLine("Task 14. Get games with their price-to-rating ratio");
            Console.WriteLine("==================================");

            var pricePerRating = games.Select(g => new
            {
                g.Title,
                PricePerRating = g.Price / g.Rating
            });

            foreach (var game in pricePerRating)
            {
                Console.WriteLine($"Rating: {game.PricePerRating}, Title: {game.Title}");
            }
            Console.WriteLine("==================================");

            // Task 15. Get all review scores across all games of CD Projekt Red

            Console.WriteLine("Task 15. Get all review scores across all games of CD Projekt Red");
            Console.WriteLine("==================================");

            var allScoresByDeveloper = games
             .Where(g => g.Developer == "CD Projekt Red")
             .SelectMany(g => g.Reviews.Select(r => new { g.Developer, r.Score }));

            foreach (var item in allScoresByDeveloper)
            {
                Console.WriteLine($"Developer: {item.Developer}, Score: {item.Score}");
            }
            Console.WriteLine("==================================");

            // Task 16. Find the average review score for games released after 2020

            Console.WriteLine("Task 16. Find the average review score for games released after 2020");
            Console.WriteLine("==================================");

            var avgNewGameScore = games
                .Where(g => g.ReleaseDate.Year > 2020)
                .SelectMany(g => g.Reviews)
                .Average(r => r.Score);
            Console.WriteLine($"The average review score for games released after 2020: {avgNewGameScore}");
            Console.WriteLine("==================================");

            // Task 17. Count games by genre

            Console.WriteLine("Task 17. Count games by genre");
            Console.WriteLine("==================================");

            var gamesByGenre = games
                .GroupBy(g => g.Genre)
                .Select(group => new { Genre = group.Key, Count = group.Count() });

            foreach (var game in gamesByGenre)
            {
                Console.WriteLine($"Genre: {game.Genre}, Count: {game.Count}");
            }
            Console.WriteLine("==================================");

            // Task 18. Find games that have all verified purchase reviews

            Console.WriteLine("Task 18. Find games that have all verified purchase reviews");
            Console.WriteLine("==================================");

            var allVerifiedGames = games
            .Where(g => g.Reviews.All(r => r.IsVerifiedPurchase));

            foreach (var game in allVerifiedGames)
            {
                Console.WriteLine($"Game: {game.Title}");
            }
            Console.WriteLine("==================================");

            // Task 19. Find all unique tags across all games

            Console.WriteLine("Task 19. Find all unique tags across all games");
            Console.WriteLine("==================================");

            var uniqueTags = games.SelectMany(g => g.Tags)
                .Distinct()
                .OrderBy(t => t);

            foreach (var tag in uniqueTags)
            {
                Console.WriteLine($"Tag: {tag}");
            }
            Console.WriteLine("==================================");

            // Task 20. Find the top 3 games by sales count with their developer

            Console.WriteLine("Task 20. Find the top 3 games by sales count with their developer");
            Console.WriteLine("==================================");

            var topSellers = games.OrderByDescending(g => g.SalesCount)
                .Take(3)
                .Select(g => new { g.Title, g.Developer, g.SalesCount });

            foreach (var game in topSellers)
            {
                Console.WriteLine($"Game: {game.Title}, Developer: {game.Developer}, Sales count: {game.SalesCount}");
            }
            Console.WriteLine("==================================");


            // Querry Syntax
            // Task 21. Get all games sorted by title 

            Console.WriteLine("Task 21. Get all games sorted by title");
            Console.WriteLine("==================================");

            var sortedGames = from game in games
                              orderby game.Title
                              select game;

            foreach (var game in sortedGames)
            {
                Console.WriteLine($"Game: {game.Title}");
            }
            Console.WriteLine("==================================");

            // Task 22. Find free games

            Console.WriteLine("Task 22. Find free games");
            Console.WriteLine("==================================");

            var freeGames = from game in games
                            where game.Price == 0
                            select game;

            foreach (var game in freeGames)
            {
                Console.WriteLine($"Game: {game.Title}, Price: {game.Price}");
            }
            Console.WriteLine("==================================");

            // Task 23. Get titles of multiplayer games

            Console.WriteLine("Task 23. Get titles of multiplayer games");
            Console.WriteLine("==================================");

            var multiplayerGames = from game in games
                                   where game.IsMultiplayer
                                   select game;

            foreach (var game in multiplayerGames)
            {
                Console.WriteLine($"Game: {game.Title}");
            }
            Console.WriteLine("==================================");


            // Task 24. Find games released in 2023

            Console.WriteLine("Task 24. Find games released in 2023");
            Console.WriteLine("==================================");

            var games2023 = from game in games
                            where game.ReleaseDate.Year == 2023
                            select game;

            foreach (var game in games2023)
            {
                Console.WriteLine($"Game: {game.Title}");
            }
            Console.WriteLine("==================================");

            // Task 25. Find games with verified reviews and calculate average scores. You must use both Querry and Method syntax

            Console.WriteLine("Task 25. Find games with verified reviews and calculate average scores");
            Console.WriteLine("==================================");

            var verifiedScores = from game in games
                                 let verifiedReviews = game.Reviews.Where(r => r.IsVerifiedPurchase)
                                 where verifiedReviews.Any()
                                 select new
                                 {
                                     game.Title,
                                     AverageVerifiedScore = verifiedReviews.Average(r => r.Score),
                                     VerifiedReviewCount = verifiedReviews.Count()
                                 };

            foreach (var score in verifiedScores)
            {
                Console.WriteLine($"Game: {score.Title}, Verified reviews: {score.VerifiedReviewCount}," +
                    $" Average score: {score.AverageVerifiedScore}");
            }
            Console.WriteLine("==================================");




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
    }
}
