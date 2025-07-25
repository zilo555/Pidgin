namespace Pidgin.Incremental;

/// <summary>
/// Represents the result of an incremental parse operation,
/// supporting shifting of locations in response to edits.
/// </summary>
/// <typeparam name="T">
/// The type of the object implementing this interface.
/// </typeparam>
public interface IShiftable<T>
    where T : class, IShiftable<T>
{
    /// <summary>
    /// Returns a copy of this result with all stored
    /// locations shifted by the specified amount.
    /// </summary>
    /// <remarks>
    /// An implementation can simply return <c>this</c> if
    /// the object does not contain any source locations
    /// that would need shifting.
    /// </remarks>
    /// <param name="amount">The amount by which to shift locations.</param>
    /// <returns>A new result with shifted locations.</returns>
    public T ShiftBy(long amount);
}
