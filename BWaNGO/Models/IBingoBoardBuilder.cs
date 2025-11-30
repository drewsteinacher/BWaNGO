namespace BWaNGO.Models;

public interface IBingoBoardBuilder
{
    /// <summary>
    /// Provides the labels to use for the bingo board squares
    /// </summary>
    /// <param name="labels"></param>
    /// <returns></returns>
    public BingoBoardBuilder FromLabels(List<string> labels);

    /// <summary>
    /// Specifies the square size of the bingo board (e.g., 5 for a 5x5 board)
    /// </summary>
    /// <param name="squareSize"></param>
    /// <returns></returns>
    public BingoBoardBuilder WithSize(int squareSize);

    /// <summary>
    /// Specifies a free square at the given square's coordinates
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public BingoBoardBuilder WithFreeSquare(int row, int column);

    /// <summary>
    /// Specifies a winning pattern by the coordinates of the squares that make up the pattern
    /// </summary>
    /// <param name="patternCoordinates"></param>
    /// <returns></returns>
    public BingoBoardBuilder WithWinningPattern(List<(int, int)> patternCoordinates);
    
    /// <summary>
    /// Builds the board to the specifications provided
    /// </summary>
    /// <returns></returns>
    public BingoBoard Build();
}