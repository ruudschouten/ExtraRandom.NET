namespace ExtraRandom.PRNG;

/// <summary>
///     Base class for Random Number Generator that the internal state produces 64-bit output.
/// </summary>
public abstract class BasePrng64
{
    // TODO: Look at https://github.com/bgrainger/RomuRandom
    // TODO: Look at https://github.com/martinothamar/Xoroshiro128Plus
    
    /// <summary>
    /// Gets the internal state of RNG.
    /// </summary>
    protected ulong[] State { get; set; } = new ulong[16];

    /// <summary>
    /// Set RNG internal state manually.
    /// </summary>
    /// <param name="seed">
    /// Number to generate the random numbers.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Array of seed is null or empty.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Seed amount must same as the internal state amount.
    /// </exception>
    public virtual void SetSeed(params ulong[] seed)
    {
        if (seed == null || seed.Length == 0)
        {
            throw new ArgumentNullException(nameof(seed), "Seed can't be null or empty.");
        }

        if (seed.Length < State.Length)
        {
            throw new ArgumentException($"Seed needs to be at least {State.Length} numbers.", nameof(seed));
        }

        var length = seed.Length > State.Length ? State.Length : seed.Length;
        Array.Copy(seed, 0, State, 0, length);
    }

    /// <summary>
    /// Seed with <see cref="System.Security.Cryptography.RandomNumberGenerator"/>.
    /// </summary>
    public abstract void Reseed();

    /// <summary>
    /// Generate next random number.
    /// </summary>
    /// <returns>
    /// A 64-bit unsigned integer.
    /// </returns>
    public abstract ulong Next();
}