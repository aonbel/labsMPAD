using Domain.Models;

namespace UI.Services.GameService;

public interface IGameService
{
    public Task<ResponseData<List<Game>?>> GetAllAsync();

    public Task<ResponseData<ListModel<Game>>> GetByPageAsync(int pageSize, int pageNumber = 1);

    public Task<ResponseData<ListModel<Game>>> GetByGenreIdAndPageAsync(int genreId, int pageSize, int pageNumber = 1);

    public Task<ResponseData<Game>> GetByIdAsync(int id);

    public Task UpdateAsync(Game game, IFormFile? file);

    public Task DeleteAsync(int id);

    public Task CreateAsync(Game game, IFormFile? file);
}