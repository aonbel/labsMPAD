using Microsoft.AspNetCore.Mvc;
using UI.Services.GameService;

namespace UI.Controllers;

public class GameController (
    IGameService gameService,
    IGameGenreService gameGenreService): Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        var response = await gameService.GetByPageAsync(1, 10);

        if (!response.Successful)
        {
            return NotFound(response.ErrorMessage);
        }
        
        var games = response.Data!;
        
        return View(games.Items);
    }
}