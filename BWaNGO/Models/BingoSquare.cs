namespace BWaNGO.Models;

public class BingoSquare(string label, bool isMarked = false)
{
    /// <summary>
    /// The label/text shown on the square
    /// </summary>
    public string Label { get; } = label;

    /// <summary>
    /// Identifiers for the pattern conditions this square is a part of
    /// </summary>
    public List<int> PatternIds { get; set; } = [];

    /// <summary>
    /// If the square is currently marked/selected
    /// </summary>
    public bool IsMarked { get; private set; } = isMarked;

    /// <summary>
    /// Marks the square to ensure any custom logic is executed
    /// </summary>
    public virtual void Mark()
    {
        IsMarked = true;
    }

    /// <summary>
    /// Unmarks the square to ensure any custom logic is executed
    /// </summary>
    public virtual void UnMark()
    {
        IsMarked = false;
    }
}