using System.Text.Json;
using AwesomeAssertions;
using BWaNGO.Models;

namespace BWaNGO.Tests.Models;

public class BingoSquareTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };
    
    private const string SerializedJson =
        """
        {
          "$type": "BingoSquare",
          "Label": "Regular",
          "PatternIds": [
            1,
            2,
            3
          ],
          "IsMarked": false
        }
        """;

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
    
    [Fact]
    public void ShouldDeserializeCorrectly()
    {
        var square = JsonSerializer.Deserialize<BingoSquare>(SerializedJson);
        
        square.Should().NotBeNull();
        square.Should().BeOfType<BingoSquare>();
        square.Label.Should().Be("Regular");
        square.IsMarked.Should().BeFalse();
        square.PatternIds.Should().BeEquivalentTo([1, 2, 3]);
    }
    
    [Fact]
    public void ShouldSerializeCorrectly()
    {
        var square = new BingoSquare("Regular")
        {
            PatternIds = [1, 2, 3]
        };
        
        var json = JsonSerializer.Serialize(square, JsonSerializerOptions);
        
        json.Should().Be(SerializedJson);
    }
}