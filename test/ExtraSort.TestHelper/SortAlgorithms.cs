﻿using Xunit;

namespace ExtraSort.TestHelper;

public class SortAlgorithms : TheoryData<ISortAlgorithm>
{
    public SortAlgorithms()
    {
        Add(new BinaryInsertionSort());
        Add(new InsertionSort());
        Add(new MergeSort());
    }
}
