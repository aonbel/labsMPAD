using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UI.Services.GameService;

namespace UI.Controllers;

public class GameController(
    IGameService gameService,
    IGameGenreService gameGenreService,
    IConfiguration configuration) : Controller
{
    // GET
    public async Task<IActionResult> Index(int? gameGenreId, int pageNumber = 1)
    {
        var gameGenresResponse = await gameGenreService.GetAllAsync();

        if (!gameGenresResponse.Successful)
        {
            return NotFound(gameGenresResponse.ErrorMessage);
        }

        ViewBag.GameGenres = gameGenresResponse.Data;

        if (gameGenreId is not null)
        {
            var currentGameGenreResponse = await gameGenreService.GetByIdAsync(gameGenreId ?? 0);

            if (!currentGameGenreResponse.Successful)
            {
                return NotFound(currentGameGenreResponse.ErrorMessage);
            }
            
            ViewBag.CurrentGameGenre = currentGameGenreResponse.Data;
        }
        else
        {
            ViewBag.CurrentGameGenre = null;
        }
        
        ResponseData<ListModel<Game>> gamesResponse;

        var itemsPerPage = int.Parse(configuration["Application:ItemsPerPage"] ?? throw new InvalidOperationException());

        if (gameGenreId is not null)
        {
            gamesResponse = await gameService.GetByGenreIdAndPageAsync((int)gameGenreId, pageNumber, itemsPerPage);
        }
        else
        {
            gamesResponse = await gameService.GetByPageAsync(pageNumber, itemsPerPage);
        }

        if (!gamesResponse.Successful)
        {
            return NotFound(gamesResponse.ErrorMessage);
        }

        var games = gamesResponse.Data!;

        return View(games);
    }
}