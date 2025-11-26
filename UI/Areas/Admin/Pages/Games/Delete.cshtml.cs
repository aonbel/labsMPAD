using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Areas.Admin.Views.Games;

public class DeleteModel(IGameService gameService) : PageModel
{
    [BindProperty] public Game Game { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var game = await gameService.GetByIdAsync(id.Value);

        if (!game.Successful) return NotFound(game.ErrorMessage ?? string.Empty);

        Game = game.Data!;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null) return NotFound();

        var gameGetResponse = await gameService.GetByIdAsync(id.Value);

        if (!gameGetResponse.Successful) return NotFound(gameGetResponse.ErrorMessage ?? string.Empty);

        await gameService.DeleteAsync((int)id);

        return RedirectToPage("./Index");
    }
}