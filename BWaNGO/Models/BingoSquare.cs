namespace BWaNGO.Models;

public class BingoSquare(string label)
{
    /// <summary>
    /// The label/text shown on the square
    /// </summary>
    public string Label { get; } = label;
    
    /// <summary>
    /// If set, an identifier for the pattern condition this square is part of
    /// </summary>
    public int? PatternId { get; set; }
    
    /// <summary>
    /// If the square is currently marked/selected
    /// </summary>
    public bool IsMarked { get; private set; }
    
    /// <summary>
    /// If the square is part of a winning solution
    /// </summary>
    public bool IsPartOfWinningSolution { get; set; }

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