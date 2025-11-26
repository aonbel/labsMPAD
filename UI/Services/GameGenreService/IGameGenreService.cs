using Domain.Models;

namespace UI.Services.GameGenreService;

public interface IGameGenreService
{
    public Task<ResponseData<List<GameGenre>>> GetAllAsync();

    public Task<ResponseData<GameGenre>> GetByIdAsync(int id);
}