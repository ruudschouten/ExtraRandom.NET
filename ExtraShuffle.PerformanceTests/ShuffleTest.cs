using Bogus;
using ExtraShuffle.TestHelper;

namespace ExtraShuffle.PerformanceTests;

public class ShuffleTest
{
    [Theory]
    [ClassData(typeof(ShuffleMethods))]
    public void Performance(Shuffle shuffle)
    {
        const int iterations = 10_000;

        Randomizer.Seed = new Random(500);

        var artists = new Artist[]
        {
            new("Adele"),
            new("Metallica"),
            new("Manchester Orchestra"),
            new("Kishi Bashi"),
            new("The Beatles"),
            new("The Rolling Stones"),
            new("The Who"),
            new("The Kinks"),
            new("The Doors"),
            new("The Beach Boys")
        };
        var songFaker = new Faker<Song>()
            .RuleFor(x => x.Artist, (faker, _) => faker.PickRandom(artists))
            .RuleFor(x => x.Title, (faker, _) => faker.Lorem.Word())
            .RuleFor(x => x.Album, (faker, _) => faker.Lorem.Word())
            .RuleFor(x => x.Genre, (faker, _) => faker.Music.Genre());

        var playlist = songFaker.Generate(5000);

        for (var i = 0; i < iterations; i++)
        {
            shuffle.Method.Invoke(playlist);
        }
    }
}
