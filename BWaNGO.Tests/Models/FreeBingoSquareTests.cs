using AwesomeAssertions;
using BWaNGO.Models;

namespace BWaNGO.Tests.Models;

public class FreeBingoSquareTests
{
    [Fact]
    public void Mark_Marks()
    {
        var square = new FreeBingoSquare();
        square.IsMarked.Should().BeFalse();
        
        square.Mark();
        
        square.IsMarked.Should().BeTrue();
    }

    [Fact]
    public void UnMark_DoesNotUnmark()
    {
        var square = new FreeBingoSquare();
        square.Mark();
        square.IsMarked.Should().BeTrue();
        
        square.UnMark();
        
        square.IsMarked.Should().BeTrue();
    }
}