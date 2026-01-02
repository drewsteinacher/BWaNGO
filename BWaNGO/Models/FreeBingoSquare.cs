namespace BWaNGO.Models;

public sealed class FreeBingoSquare() : BingoSquare("FREE", true)
{
    /// <inheritdoc/>
    public override void UnMark()
    {
        // No-Op; Free squares cannot be unmarked
    }
}