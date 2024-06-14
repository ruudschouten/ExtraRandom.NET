using System.Runtime.InteropServices;

namespace ExtraSort.MergeSorts;

/// <summary>
/// Represents a run within a merge sorting algorithm.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct Run(int Start, int End);
