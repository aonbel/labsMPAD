using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Areas.Admin.Views.Games;

public class DetailsModel(IGameService gameService) : PageModel
{
    public Game Game { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var gameGetResponse = await gameService.GetByIdAsync((int)id);

        if (!gameGetResponse.Successful) return NotFound(gameGetResponse.ErrorMessage ?? string.Empty);

        Game = gameGetResponse.Data!;

        return Page();
    }
}