using AwesomeAssertions;
using BWaNGO.Generation;
using BWaNGO.Models;

namespace BWaNGO.Tests.Generation;

public class BingoBoardGeneratorTests
{
    [Fact]
    public void Generate_AppliesPresetSizeFreeSquaresAndWinningPatterns()
    {
        const int size = 3;
        var freeSquares = new[] { (2, 2) };
        var winningPatterns = new List<IEnumerable<(int, int)>>
        {
            Enumerable.Range(0, size - 1).Select(c => (0, c)).ToList(), // first row
            Enumerable.Range(0, size - 1).Select(r => (r, 0)).ToList(), // first column
        };

        var preset = new TestPreset(size, freeSquares, winningPatterns);
        var labels = Enumerable.Range(0, (size - 1) * (size - 1)).Select(i => $"L{i}");

        var generator = new BingoBoardGenerator();
        var board = generator.Generate(labels, preset);

        
        board.Should().NotBeNull();
        board.Rows.Should().Be(size);
        board.Columns.Should().Be(size);
        board.GetSquare(2, 2).Should().BeOfType<FreeBingoSquare>();

        var patterns = new HashSet<int>();
        foreach (var pattern in winningPatterns)
        {
            foreach (var (row, col) in pattern)
            {
                var square = board.GetSquare(row, col);
                square.PatternId.Should().NotBeNull();

                if (square.PatternId == null) continue;
                
                square.Mark();
                patterns.Add(square.PatternId.Value);
            }
        }
        patterns.Count.Should().Be(winningPatterns.Count);
    }

    [Fact]
    public void Generate_AppliesMultipleFreeSquares()
    {
        const int size = 4;
        var freeSquares = new[] { (0, 0), (3, 3) };

        var preset = new TestPreset(size, freeSquares, [freeSquares]);
        var labels = Enumerable.Range(0, (size - 1) * (size - 1)).Select(i => $"L{i}");

        var generator = new BingoBoardGenerator();
        var board = generator.Generate(labels, preset);

        board.Should().NotBeNull();
        board.Rows.Should().Be(size);
        board.Columns.Should().Be(size);
        foreach (var (row, col) in freeSquares)
        {
            board.GetSquare(row, col).Should().BeOfType<FreeBingoSquare>();
        }
    }
}