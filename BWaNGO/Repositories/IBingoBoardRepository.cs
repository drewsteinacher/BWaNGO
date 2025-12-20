using BWaNGO.Models;

namespace BWaNGO.Repositories;

public interface IBingoBoardRepository
{
    Task<BingoBoard?> Get();

    Task SaveAsync(BingoBoard? board);
}