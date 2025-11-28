using Blazored.LocalStorage;

namespace BWaNGO.Services;

internal sealed class LabelsService : ILabelsService
{
    private const string LabelsKey = "Labels";
    
    private readonly ILocalStorageService _localStorageService;

    public static List<string> ParseLabels(string rawLabelString) =>
        rawLabelString
            .Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();

    public LabelsService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    
    public async Task<List<string>> Get()
        => ParseLabels(await GetRaw() ?? string.Empty);

    public Task<string?> GetRaw()
        => _localStorageService.GetItemAsStringAsync(LabelsKey).AsTask();

    public Task SaveAsync(string rawLabelString)
        => _localStorageService.SetItemAsStringAsync(LabelsKey, rawLabelString).AsTask();

    public Task SaveAsync(List<string> labels)
        => SaveAsync(string.Join(Environment.NewLine, labels));
}