using System.Diagnostics.CodeAnalysis;
using Bogus;
using ExtraShuffle.TestHelper;

namespace ExtraShuffle.PerformanceTests;

[SuppressMessage(
    "Blocker Code Smell",
    "S2699:Tests should include assertions",
    Justification = "These are performance tests."
)]
public class ShuffleTest
{
    [Theory]
    [ClassData(typeof(ShuffleFunctions))]
    public void Performance(ShuffleFunction shuffle)
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
