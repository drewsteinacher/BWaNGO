using System.Text.Json;
using AwesomeAssertions;
using BWaNGO.Models;

namespace BWaNGO.Tests.Models;

public class FreeBingoSquareTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    private const string SerializedJson =
        """
        {
          "$type": "FreeBingoSquare",
          "Label": "FREE",
          "PatternIds": [
            1,
            2,
            3
          ],
          "IsMarked": true
        }
        """;

    [Fact]
    public void Mark_Marks()
    {
        var square = new FreeBingoSquare();
        square.IsMarked.Should().BeTrue();
        
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
    
    [Fact]
    public void ShouldDeserializeCorrectly()
    {
        var square = JsonSerializer.Deserialize<BingoSquare>(SerializedJson);
        
        square.Should().NotBeNull();
        square.Should().BeOfType<FreeBingoSquare>();
        square.Label.Should().Be("FREE");
        square.IsMarked.Should().BeTrue();
        square.PatternIds.Should().BeEquivalentTo([1, 2, 3]);
    }
    
    [Fact]
    public void ShouldSerializeCorrectly()
    {
        var square = new FreeBingoSquare
        {
            PatternIds = [1, 2, 3]
        };
        
        var json = JsonSerializer.Serialize<BingoSquare>(square, JsonSerializerOptions);
        
        json.Should().Be(SerializedJson);
    }
}