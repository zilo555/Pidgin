namespace Pidgin.Incremental;

/// <summary>
/// Represents a portion of an input stream,
/// defined by a start position and a length.
/// </summary>
/// <param name="Start">
/// The starting location of the range.
/// </param>
/// <param name="Length">The length of the range.</param>
public record LocationRange(long Start, long Length) : IShiftable<LocationRange>
{
    /// <summary>
    /// The location immediately after the end of the range.
    /// </summary>
    public long End => Start + Length;

    internal bool Contains(long location)
        => location >= Start && location <= End;

    // NOTE: Edits at ends of cached results
    // =====================================
    //
    // An edit that extends an existing node
    //     |oldText|
    //             |edit|
    // might require reparsing. (eg, "class" -> "classsss")
    //
    // However, if the edit that prepends an existing node,
    //          |oldText|
    //     |edit|
    // if the parser consumes the edit and still reaches the
    // same state (ie, same parser instance) at the end of the
    // edit, we can use the cached parse of oldText.
    internal bool OverlapsOrExtends(LocationRange other)
        => End > other.Start && other.End >= Start;

    /// <inheritdoc cref="IShiftable{T}.ShiftBy(long)" />
    public LocationRange ShiftBy(long amount)
        => this with { Start = Start + amount };
}
