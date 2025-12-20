using BWaNGO.Generation.Presets;
using BWaNGO.Models;

namespace BWaNGO.Generation;

public interface IBingoBoardGenerator
{
    BingoBoard Generate(IEnumerable<string> labels, IBingoPreset preset);
}