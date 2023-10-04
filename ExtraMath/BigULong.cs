using System.Runtime.InteropServices;

namespace ExtraMath;

/// <summary>
/// Represents a 128-bit ulong number.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public struct BigULong
{
    /// <summary>
    /// High bit of the 128-bit number.
    /// </summary>
    public ulong High;

    /// <summary>
    /// Low bit of the 128-bit number.
    /// </summary>
    public ulong Low;
}
