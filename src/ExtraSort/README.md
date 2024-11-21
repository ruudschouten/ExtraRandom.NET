## ExtraSort

Added because the `WeightedRandom` required a sorted list of weights, and I thought it would be fun to add a few sorting algorithms it could use instead of the `OrderBy` LINQ method.

The sorts added now are;
- Binary Insertion Sort
- Insertion Sort
- Merge Sort
- Tim Sort
- Power Sort
- Peek Sort

These are added using a `Sort` extension method for `IList<T>`, in which a instance of `ISortAlgorithm` must be passed.
This instance is then used to sort the current list.