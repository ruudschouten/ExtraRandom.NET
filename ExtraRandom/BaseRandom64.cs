namespace ExtraRandom;

/// <summary>
/// Base class for random number generator in which the internal state produces a 64-bit output.
/// </summary>
public abstract class BaseRandom64 : BaseRandom
{
    /// <summary>
    /// Internal state of the RNG.
    /// </summary>
    protected ulong[] State;

    /// <summary>
    /// Size is 8 bytes.
    /// </summary>
    protected const byte Size = 8;

    /// <summary>
    /// Reseed the <see cref="State"/>.
    /// </summary>
    public abstract void Reseed();

    /// <summary>
    /// Manually set the internal RNG state.
    /// </summary>
    /// <param name="seed">Seed to use to generate random numbers.</param>
    /// <exception cref="ArgumentNullException">Array of seed is null or empty.</exception>
    /// <exception cref="ArgumentException">Seed amount must be at least the same lenght as the <see cref="State"/> length.</exception>
    public virtual void SetSeed(params ulong[] seed)
    {
        if (seed == null || seed.Length == 0)
        {
            throw new ArgumentNullException(nameof(seed), "Seed can't be null or empty.");
        }

        if (seed.Length != State.Length)
        {
            throw new ArgumentException($"Seed must be at least {State.Length} long", nameof(seed));
        }

        // Change the length to whichever field is larger.
        var length = seed.Length > State.Length ? State.Length : seed.Length;
        Array.Copy(seed, 0, State, 0, length);
    }

    /// <summary>
    /// Generate next random number.
    /// </summary>
    /// <returns>A 64-bit unsigned integer.</returns>
    public abstract ulong Next();

    /// <summary>
    /// Generate <see cref="bool"/> value.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> or <see langword="false"/>.
    /// </returns>
    public abstract bool NextBoolean();

    /// <summary>
    /// Generate a non-negative random integer.
    /// </summary>
    /// <returns>A 8-bit unsigned integer that is greater than or equal to 0.</returns>
    public abstract byte NextByte();
    
    public virtual byte NextByte(byte min, byte max)
}