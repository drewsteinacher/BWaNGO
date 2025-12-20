using BWaNGO.Generation.Presets;

namespace BWaNGO.Tests.Generation;

internal class TestPreset(
    int size,
    IEnumerable<(int, int)> freeSquares,
    IEnumerable<IEnumerable<(int, int)>> winningPatterns)
    : IBingoPreset
{
    public string DefaultName => "Test Preset";

    public int Size { get; } = size;

    public IEnumerable<(int, int)> FreeSquares { get; } = freeSquares;

    public IEnumerable<IEnumerable<(int, int)>> WinningPatterns { get; } = winningPatterns;
}