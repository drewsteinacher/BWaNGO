using System.Reflection.Emit;

namespace BWaNGO.Repositories;

public interface ILabelsRepository
{
    Task<List<string>> Get();
    
    Task<string?> GetRaw();
    
    Task SaveAsync(List<string> labels);
    
    Task SaveAsync(string rawLabelString);
}