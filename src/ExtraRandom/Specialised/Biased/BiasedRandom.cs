using System.Runtime.InteropServices;
using ExtraRandom.Validator;

namespace ExtraRandom.Specialised.Biased;

/// <summary>
/// Specialised random generator in which you can specify a bias.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct BiasedRandom : IRandom
{
    /// <summary>
    /// PRNG instance to use for the random rolls.
    /// </summary>
    private readonly IRandom _random;

    /// <summary>
    /// What type of results to favour.
    /// </summary>
    private readonly IBias _bias;

    /// <summary>
    /// Amount of rolls to perform before picking a result closest to the <see name="_bias"/>.
    /// </summary>
    private readonly int _rolls;

    /// <summary>
    /// Initializes a new instance of the <see cref="BiasedRandom"/> struct.
    /// </summary>
    /// <param name="random">PRNG instance to use for the random rolls.</param>
    /// <param name="bias">What type of results to favour.</param>
    /// <param name="rolls">Amount of rolls to perform before picking a result closest to the <paramref name="bias"/>.</param>
    public BiasedRandom(IRandom random, IBias bias, int rolls = 1)
    {
        _random = random;
        _bias = bias;
        if (rolls < 1)
            throw new ArgumentOutOfRangeException(nameof(rolls), "Rolls must be greater than 0.");
        _rolls = rolls;
    }

    #region IRandom interface

    /// <inheritdoc cref="IRandom.SetSeed(ulong)" />
    public void SetSeed(ulong seed)
    {
        _random.SetSeed(seed);
    }

    /// <inheritdoc cref="IRandom.SetSeed(ulong[])" />
    public void SetSeed(params ulong[] seed)
    {
        _random.SetSeed(seed);
    }

    /// <inheritdoc />
    public bool NextBoolean()
    {
        return _random.NextBoolean();
    }

    /// <inheritdoc />
    public byte NextByte()
    {
        return NextByte(byte.MinValue, byte.MaxValue);
    }

    /// <inheritdoc/>
    public byte NextByte(byte min, byte max)
    {
        return (byte)Roll(min, max);
    }

    /// <inheritdoc />
    public int NextInt()
    {
        return NextInt(int.MinValue, int.MaxValue);
    }

    /// <inheritdoc />
    public int NextInt(int min, int max)
    {
        return (int)Roll(min, max);
    }

    /// <inheritdoc />
    public uint NextUInt()
    {
        return NextUInt(uint.MinValue, uint.MaxValue);
    }

    /// <inheritdoc />
    public uint NextUInt(uint min, uint max)
    {
        return (uint)Roll(min, max);
    }

    /// <inheritdoc />
    public long NextLong()
    {
        return NextLong(long.MinValue, long.MaxValue);
    }

    /// <inheritdoc cref="IRandom.NextLong()"/>
    public long NextLong(long min, long max)
    {
        return (long)Roll(min, max);
    }

    /// <inheritdoc />
    public ulong NextULong()
    {
        return NextULong(ulong.MinValue, ulong.MaxValue);
    }

    /// <inheritdoc />
    public ulong NextULong(ulong min, ulong max)
    {
        return (ulong)Roll(min, max);
    }

    /// <inheritdoc />
    public double NextDouble()
    {
        return NextDouble(0, 1.0);
    }

    /// <inheritdoc />
    public double NextDouble(double min, double max)
    {
        return Roll(min, max);
    }

    #endregion

    private double Roll(double min, double max)
    {
        NextInRangeValidator.ValidateRange(min, max);

        var closestForBias = _bias.SetBiasValues(min, max);

        for (var i = 0; i < _rolls; i++)
        {
            var roll = _bias.Roll(in _random, min, max);
            closestForBias = _bias.GetClosest(roll, closestForBias);
        }

        return closestForBias;
    }
}