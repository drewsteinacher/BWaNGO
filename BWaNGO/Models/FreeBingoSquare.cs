namespace BWaNGO.Models;

public sealed class FreeBingoSquare() : BingoSquare("FREE")
{
    /// <inheritdoc/>
    public override void UnMark()
    {
        // No-Op; Free squares cannot be unmarked
    }
}