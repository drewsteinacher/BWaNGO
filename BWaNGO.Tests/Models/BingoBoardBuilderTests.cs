using AwesomeAssertions;
using BWaNGO.Models;

namespace BWaNGO.Tests.Models;

public class BingoBoardBuilderTests
{
    // Note: This test may occasionally fail due to random chance, but the probability is extremely low.
    [Fact]
    public void FromLabels_CreatesShuffledBoard()
    {
        const int squareSize = 5;
        var labels = Enumerable.Range(0, squareSize * squareSize)
            .Select(i => $"label-{i}")
            .ToList();
        var board1 = new BingoBoardBuilder().FromLabels(labels).WithSize(squareSize).Build();
        var board2 = new BingoBoardBuilder().FromLabels(labels).WithSize(squareSize).Build();

        var nonIdenticalCount = 0;
        for (var row = 0; row < squareSize; row++) {
            for (var column = 0; column < squareSize; column++)
            {
                var square1 = board1.GetSquare(row, column);
                var square2 = board2.GetSquare(row, column);
                if (square1.Label != square2.Label)
                {
                    nonIdenticalCount++;
                }
            }
        }

        nonIdenticalCount.Should().BeGreaterThan(0);
    }

    [Fact]
    public void FromLabels_PadsBoardWithRandomSelection()
    {
        const int squareSize = 3;
        List<string> labels = ["A", "B", "C"];
        var board = new BingoBoardBuilder().FromLabels(labels).WithSize(squareSize).Build();

        List<BingoSquare> allSquares = [];
        for (var row = 0; row < board.Rows; row++) {
            for (var column = 0; column < board.Columns; column++)
            {
                var square = board.GetSquare(row, column);
                allSquares.Add(square);
            }
        }
        
        allSquares.Count.Should().Be(squareSize * squareSize);

        var allLabels = allSquares.Select(s => s.Label).Distinct().Order().ToList();
        allLabels.Should().BeEquivalentTo(labels.Distinct().OrderBy(l => l).ToList());
    }
    
    // WithSize_DoesNotAllowZero
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WithSize_DoesNotAllowZeroOrNegativeValues(int squareSize)
    {
        var action = () => new BingoBoardBuilder().WithSize(squareSize).Build();
        
        action.Should().Throw<ArgumentException>().WithMessage("Square size must be at least 1");
    }
    
    // WithSize_SetsRowsAndColumns
    [Fact]
    public void WithSize_SetsRowsAndColumns()
    {
        const int squareSize = 3;

        var board = new BingoBoardBuilder()
            .FromLabels(Enumerable.Range(0, squareSize * squareSize).Select(i => i.ToString()).ToList())
            .WithSize(squareSize)
            .Build();

        board.Rows.Should().Be(squareSize);
        board.Columns.Should().Be(squareSize);
    }

    // WithFreeSquare_DoesNotAllowNegativeCoordinates
    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    public void WithFreeSquare_DoesNotAllowNegativeCoordinates(int row, int column)
    {
        var action = () => new BingoBoardBuilder().WithFreeSquare(row, column).Build();

        action.Should().Throw<ArgumentException>().WithMessage("Square coordinates must be non-negative");
    }

    // WithFreeSquare_AddsFreeSquareWithValidCoordinates
    [Fact]
    public void WithFreeSquare_AddsFreeSquareWithValidCoordinates()
    {
        const int squareSize = 3;
        const int freeRow = 1;
        const int freeColumn = 2;

        var board = new BingoBoardBuilder()
            .FromLabels(Enumerable.Range(0, squareSize * squareSize).Select(i => i.ToString()).ToList())
            .WithSize(squareSize)
            .WithFreeSquare(freeRow, freeColumn)
            .Build();

        var freeSquare = board.GetSquare(freeRow, freeColumn);
        freeSquare.Should().BeOfType<FreeBingoSquare>();
    }
    
    // WithWinningPattern_SetsUniquePatternIds
    [Fact]
    public void WithWinningPattern_SetsUniquePatternIds()
    {
        const int squareSize = 3;
        
        List<(int, int)> pattern1 = [(0, 0), (0, 1), (0, 2)];
        List<(int, int)> pattern2 = [(1, 0), (1, 1), (1, 2)];
        
        var board = new BingoBoardBuilder()
            .FromLabels(Enumerable.Range(0, squareSize * squareSize).Select(i => i.ToString()).ToList())
            .WithSize(squareSize)
            .WithWinningPattern(pattern1)
            .WithWinningPattern(pattern2)
            .Build();
        
        var patternId1 = board.GetSquare(pattern1[0].Item1, pattern1[0].Item2).PatternId;
        var patternId2 = board.GetSquare(pattern2[0].Item1, pattern2[0].Item2).PatternId;
        
        patternId1.Should().NotBeNull();
        patternId2.Should().NotBeNull();
        patternId1.Should().NotBe(patternId2);
    }
}