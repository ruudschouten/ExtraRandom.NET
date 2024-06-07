namespace ExtraRandom.Specialised.Biased;

/// <summary>
/// Interface for a bias to use in the <see cref="BiasedRandom"/>.
/// </summary>
public interface IBias
{
    /// <summary>
    /// Set the bias values which are used by all proceeding rolls.
    /// </summary>
    /// <returns>the value each roll should try to beat by rolling closer to the bias.</returns>
    double SetBiasValues(double min, double max);

    /// <summary>
    /// Roll for a new value between <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="random">random implementation to use.</param>
    /// <param name="min">inclusive lower bound.</param>
    /// <param name="max">exclusive upper bound.</param>
    /// <returns></returns>
    double Roll(in IRandom random, double min, double max);

    /// <summary>
    /// Check if the <paramref name="roll"/> was closer to the bias that the current <paramref name="closest"/>.
    /// </summary>
    /// <param name="roll">new roll.</param>
    /// <param name="closest">current closest roll to the bias.</param>
    /// <returns></returns>
    double GetClosest(double roll, double closest);
}
