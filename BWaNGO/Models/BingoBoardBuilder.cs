namespace BWaNGO.Models;

public class BingoBoardBuilder : IBingoBoardBuilder
{
    private List<BingoSquare> Squares { get; set; } = [];
    
    private int Rows { get; set; }

    private int Columns { get; set; }
    
    private Dictionary<(int, int), FreeBingoSquare> CoordinateToFreeSquare { get; set; } = [];
    
    private List<List<(int, int)>> WinningPatternCoordinates { get; set; } = [];
    
    public BingoBoardBuilder FromLabels(List<string> labels)
    {
        Squares = labels.Select(label => new BingoSquare(label)).ToList();
        return this;
    }

    public BingoBoardBuilder WithSize(int squareSize)
    {
        if (squareSize < 1)
        {
            throw new ArgumentException("Square size must be at least 1");
        }

        Rows = squareSize;
        Columns = squareSize;

        return this;
    }

    public BingoBoardBuilder WithFreeSquare(int row, int column)
    {
        if (row < 0 || column < 0)
        {
            throw new ArgumentException("Square coordinates must be non-negative");
        }

        CoordinateToFreeSquare.Add((row, column), new FreeBingoSquare());

        return this;
    }

    public BingoBoardBuilder WithWinningPattern(List<(int, int)> patternCoordinates)
    {
        WinningPatternCoordinates.Add(patternCoordinates);
        
        return this;
    }

    public BingoBoard Build()
    {
        // Pad the squares if there are not enough to fill the board
        var totalSquaresNeeded = Rows * Columns;
        while (Squares.Count < totalSquaresNeeded) {
            var randomIndex = Random.Shared.Next(Squares.Count);
            Squares.Add(new BingoSquare(Squares[randomIndex].Label));
        }
        
        // Shuffle the squares
        var arr = Squares.ToArray();
        Random.Shared.Shuffle(arr);
        Squares = arr.ToList();
        
        // Create the board squares, inserting free squares as specified
        var boardSquares = new List<BingoSquare>();
        for (var i = 0; i < Rows; i++) {
            for (var j = 0; j < Columns; j++)
            {
                // Add free squares at specified coordinates
                if (CoordinateToFreeSquare.TryGetValue((i, j), out var freeSquare))
                {
                    boardSquares.Add(freeSquare);
                    continue;
                }

                // Add the next square from the shuffled list
                boardSquares.Add(Squares[i * Rows + j]);
            }
        }

        // For each pattern, step through the coordinates and assign PatternId to the squares at those coordinates
        for (var patternIndex = 0; patternIndex < WinningPatternCoordinates.Count; patternIndex++)
        {
            var coordinateList = WinningPatternCoordinates[patternIndex];
            foreach (var (row, column) in coordinateList)
            {
                boardSquares[row * Rows + column].PatternId = patternIndex;
            }
        }

        return new BingoBoard(boardSquares, Rows, Columns);
    }
}