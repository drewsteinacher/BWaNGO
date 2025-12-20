using BWaNGO.Generation.Presets;
using BWaNGO.Models;

namespace BWaNGO.Generation;

// TODO: Consider factoring this stuff and the models out to a Core library. Might be overkill.
public class BingoBoardGenerator : IBingoBoardGenerator
{
    public BingoBoard Generate(IEnumerable<string> labels, IBingoPreset preset)
    {
        var builder = new BingoBoardBuilder()
            .WithSize(preset.Size)
            .FromLabels(labels.ToList());

        foreach (var (row, column) in preset.FreeSquares)
        {
            builder.WithFreeSquare(row, column);
        }

        foreach (var pattern in preset.WinningPatterns)
        {
            builder.WithWinningPattern(pattern.ToList());
        }
        
        return builder.Build();
    }
}