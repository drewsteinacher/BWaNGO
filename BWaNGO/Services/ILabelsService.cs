using System.Reflection.Emit;

namespace BWaNGO.Services;

public interface ILabelsService
{
    Task<List<string>> Get();
    
    Task<string?> GetRaw();
    
    Task SaveAsync(List<string> labels);
    
    Task SaveAsync(string rawLabelString);
}