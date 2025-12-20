using Blazored.LocalStorage;

using BWaNGO.Models;

namespace BWaNGO.Repositories;

public class BingoBoardRepository(ILocalStorageService localStorageService) : IBingoBoardRepository
{
    private const string BoardKey = "BingoBoard";

    public async Task<BingoBoard?> Get()
    {
        try
        {
            return await localStorageService.GetItemAsync<BingoBoard>(BoardKey).AsTask();
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error retrieving board from local storage, clearing:" + exception.Message);
            await SaveAsync(null);

            return null;
        }
    }

    public Task SaveAsync(BingoBoard? board)
        => localStorageService.SetItemAsync(BoardKey, board).AsTask();
}