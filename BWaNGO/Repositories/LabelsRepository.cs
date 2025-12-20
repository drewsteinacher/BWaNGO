using Blazored.LocalStorage;

namespace BWaNGO.Repositories;

internal sealed class LabelsRepository(ILocalStorageService localStorageService) : ILabelsRepository
{
    private const string LabelsKey = "Labels";

    public static List<string> ParseLabels(string rawLabelString) =>
        rawLabelString
            .Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();

    public async Task<List<string>> Get()
    {
        try
        {
            return ParseLabels(await GetRaw() ?? string.Empty);
        }
        catch (Exception exception)
        {
            await Clear(exception);
            return [];
        }
    }

    public async Task<string?> GetRaw()
    {
        try
        {
            return await localStorageService.GetItemAsStringAsync(LabelsKey).AsTask();
        }
        catch (Exception exception)
        {
            await Clear(exception);
            return null;
        }
    }

    public Task SaveAsync(string rawLabelString)
        => localStorageService.SetItemAsStringAsync(LabelsKey, rawLabelString).AsTask();

    public Task SaveAsync(List<string> labels)
        => SaveAsync(string.Join(Environment.NewLine, labels));
    
    private async Task Clear(Exception? exception)
    {
        if (exception is not null)
        {
            Console.WriteLine("Error retrieving labels from local storage, clearing: " + exception.Message);
        }

        await SaveAsync([]);
    }
}