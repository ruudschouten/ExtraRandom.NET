using System;
using System.Security.Cryptography;

namespace ExtraRandom;

public sealed class CryptoRandom : Random64, IDisposable
{
    private RandomNumberGenerator _generator;
    private bool _disposed;

    public CryptoRandom()
    {
        _generator = RandomNumberGenerator.Create();
    }

    public override void Reseed()
    {
        _generator.Dispose();
        _generator = RandomNumberGenerator.Create();
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _generator.Dispose();
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var buffer = new byte[sizeof(ulong)];
        _generator.GetBytes(buffer);
        return BitConverter.ToUInt64(buffer, 0);
    }
}