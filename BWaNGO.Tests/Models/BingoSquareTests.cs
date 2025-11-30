using AwesomeAssertions;
using BWaNGO.Models;

namespace BWaNGO.Tests.Models;

public class BingoSquareTests
{
    [Fact]
    public void Mark_Marks()
    {
        var square = new BingoSquare("Some square");
        square.IsMarked.Should().BeFalse();
        
        square.Mark();
        
        square.IsMarked.Should().BeTrue();
    }

    [Fact]
    public void UnMark_Unmarks()
    {
        var square = new BingoSquare("Another square");
        square.Mark();
        square.IsMarked.Should().BeTrue();
        
        square.UnMark();
        
        square.IsMarked.Should().BeFalse();
    }
}