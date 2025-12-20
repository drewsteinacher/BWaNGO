using AwesomeAssertions;
using BWaNGO.Models;

namespace BWaNGO.Tests.Models;

public class BingoBoardTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void Constructor_DoesNotAllow_ZeroOrNegativeDimensions(int rows, int columns)
    {
        var action = () => new BingoBoard([], rows, columns);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Constructor_DoesNotAllow_IncorrectNumberOfSquares()
    {
        List<BingoSquare> squares = [new("1")];
        
        var action = () => new BingoBoard(squares, 2, 3);

        action.Should().Throw<ArgumentException>("BingoBoard must have exactly 6 squares");
    }

    [Fact]
    public void Constructor_SetsRowsAndColumns()
    {
        const int rows = 2;
        const int cols = 2;
        List<BingoSquare> bingoSquares = [new("0,0"), new("0,1"), new("1,0"), new("1,1")];
        
        var board = new BingoBoard(bingoSquares, rows, cols);

        board.Rows.Should().Be(rows);
        board.Columns.Should().Be(cols);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    public void GetSquare_ReturnsCorrectSquare(int row, int column)
    {
        const int rows = 2;
        const int cols = 2;
        List<BingoSquare> bingoSquares = [new("0,0"), new("0,1"), new("1,0"), new("1,1")];

        var board = new BingoBoard(bingoSquares, rows, cols);
        
        var square = board.GetSquare(row, column);

        square.Label.Should().Be($"{row},{column}");
    }

    [Fact]
    public void Reset_Unmarks_AllSquares()
    {
        const int rows = 2;
        const int cols = 2;
        List<BingoSquare> bingoSquares = [new("0,0"), new("0,1"), new("1,0"), new("1,1")];

        var board = new BingoBoard(bingoSquares, rows, cols);

        // mark a couple of squares
        board.GetSquare(0, 0).Mark();
        board.GetSquare(1, 1).Mark();

        board.Reset();

        for (var row = 0; row < board.Rows; row++)
        {
            for (var column = 0; column < board.Columns; column++)
            {
                board.GetSquare(row, column).IsMarked.Should().BeFalse();
            }
        }
    }

    [Fact]
    public void MarkWinningSolutions_MarksSolution_IfPatternIsFullyMarked()
    {
        const int rows = 2;
        const int cols = 2;
        List<BingoSquare> bingoSquares = [new("0,0"), new("0,1"), new("1,0"), new("1,1")];
        
        var board = new BingoBoard(bingoSquares, rows, cols);

        // Mark and assign pattern
        List<(int, int)> patternCoordinates = [(0, 0), (0, 1)];
        foreach (var (row, column) in patternCoordinates)
        {
            var square = board.GetSquare(row, column);
            square.Mark();
            square.PatternId = 1;
            square.IsPartOfWinningSolution.Should().BeFalse();
        }
        
        var result = board.MarkWinningSolutions();
        result.Should().BeTrue();
        
        // Check that solution has been assigned
        foreach (var (row, column) in patternCoordinates)
        {
            var square = board.GetSquare(row, column);
            square.IsPartOfWinningSolution.Should().BeTrue();
        }
    }

    [Fact]
    public void MarkWinningSolutions_DoesNotMarkSolution_IfPatternIsNotFullyMarked()
    {
        const int rows = 2;
        const int cols = 2;
        List<BingoSquare> bingoSquares = [new("0,0"), new("0,1"), new("1,0"), new("1,1")];
        
        var board = new BingoBoard(bingoSquares, rows, cols);

        // Mark and assign only some of the pattern
        List<(int, int)> patternCoordinates = [(0, 0), (0, 1)];
        foreach (var (row, column) in patternCoordinates)
        {
            var square = board.GetSquare(row, column);
            if ((row, column) != patternCoordinates[0])
            {
                square.Mark();
            }

            square.PatternId = 1;
            square.IsPartOfWinningSolution.Should().BeFalse();
        }
        
        var result = board.MarkWinningSolutions();
        
        result.Should().BeFalse();
        
        // Check that solution has been not assigned
        foreach (var (row, column) in patternCoordinates)
        {
            var square = board.GetSquare(row, column);
            square.IsPartOfWinningSolution.Should().BeFalse();
        }
    }
}