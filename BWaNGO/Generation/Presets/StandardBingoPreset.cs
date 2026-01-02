namespace BWaNGO.Generation.Presets;

public class StandardBingoPreset(int size = 5) : IBingoPreset
{
    public string DefaultName { get; } = "New Board";

    public int Size { get; } = size;

    public IEnumerable<(int, int)> FreeSquares => [(Size / 2, Size / 2)];

    public IEnumerable<IEnumerable<(int, int)>> WinningPatterns => GenerateWinningPatterns();

    private IEnumerable<IEnumerable<(int, int)>> GenerateWinningPatterns()
    {
        // Diagonal top-left to bottom-right
        yield return Enumerable.Range(0, Size).Select(x => (x, x)).ToList();

        // Diagonal top-right to bottom-left
        yield return Enumerable.Range(0, Size).Select(x => (x, Size - 1 - x)).ToList();

        // Any row
        for (var r = 0; r < Size; r++)
        {
            yield return Enumerable.Range(0, Size).Select(c => (r, c)).ToList();
        }

        // Any column
        for (var c = 0; c < Size; c++)
        {
            yield return Enumerable.Range(0, Size).Select(r => (r, c)).ToList();
        }
    }
}