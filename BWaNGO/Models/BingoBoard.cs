namespace BWaNGO.Models;

public class BingoBoard
{
    public BingoBoard(List<BingoSquare> squares, int rows, int columns)
    {
        if (rows <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows), rows, null);
        }

        if (columns <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columns), columns, null);
        }

        if (squares.Count != rows * columns)
        {
            throw new ArgumentException($"BingoBoard must have exactly {rows * columns} squares");
        }

        Squares = squares;
        Rows = rows;
        Columns = columns;
    }

    public string Name { get; set; } = string.Empty;

    public int Rows { get; init; }

    public int Columns { get; init; }

    private List<BingoSquare> Squares { get; init; }
    
    public BingoSquare GetSquare(int row, int column)
        => Squares[row * Columns + column];

    public void Reset()
    {
        foreach (var square in Squares)
        {
            square.UnMark();
        }
    }

    public List<int> GetWinningPatterns()
    {
        var winningPatternIds = new List<int>();
        var patternIdToSquares = Squares
            .SelectMany(square => square.PatternIds.Select(patternId => (patternId, square)))
            .GroupBy(pair => pair.patternId)
            .ToDictionary(
                group => group.Key,
                group => group.Select(pair => pair.square).ToList()
            );

        foreach (var (patternId, squares) in patternIdToSquares)
        {
            if (squares.All(square => square.IsMarked))
            {
                winningPatternIds.Add(patternId);
            }
        }

        return winningPatternIds;
    }
}