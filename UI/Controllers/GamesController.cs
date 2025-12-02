using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UI.Extensions;

namespace UI.Controllers;

public class GamesController(
    IGameService gameService,
    IGameGenreService gameGenreService,
    IConfiguration configuration) : Controller
{
    // GET
    [Route("/GameGenre/{gameGenreId:int}")]
    [Route("")]
    public async Task<IActionResult> Index([FromRoute] int? gameGenreId, int pageNumber = 1)
    {
        var gameGenresResponse = await gameGenreService.GetAllAsync();

        if (!gameGenresResponse.Successful) return NotFound(gameGenresResponse.ErrorMessage);

        ViewBag.GameGenres = gameGenresResponse.Data;

        if (gameGenreId is not null)
        {
            var currentGameGenreResponse = await gameGenreService.GetByIdAsync(gameGenreId ?? 0);

            if (!currentGameGenreResponse.Successful) return NotFound(currentGameGenreResponse.ErrorMessage);

            ViewBag.CurrentGameGenre = currentGameGenreResponse.Data;
        }
        else
        {
            ViewBag.CurrentGameGenre = null;
        }

        ResponseData<ListModel<Game>> gamesResponse;

        var itemsPerPage =
            int.Parse(configuration["Application:RequestedItemsPerPage"] ?? throw new InvalidOperationException());

        if (gameGenreId is not null)
            gamesResponse = await gameService.GetByGenreIdAndPageAsync((int)gameGenreId, itemsPerPage, pageNumber);
        else
            gamesResponse = await gameService.GetByPageAsync(itemsPerPage, pageNumber);

        if (!gamesResponse.Successful) return NotFound(gamesResponse.ErrorMessage);

        var games = gamesResponse.Data!;

        if (Request.IsAjaxRequest()) return PartialView("_GamesMenu", games);

        return View(games);
    }
}