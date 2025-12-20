namespace BWaNGO.Generation.Presets;

public interface IBingoPreset
{
    string DefaultName { get; }

    int Size { get; }

    IEnumerable<(int, int)> FreeSquares { get; } 

    IEnumerable<IEnumerable<(int, int)>> WinningPatterns { get; }
}