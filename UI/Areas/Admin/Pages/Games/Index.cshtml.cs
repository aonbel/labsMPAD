using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Areas.Admin.Views.Games;

public class IndexModel(IGameService gameService) : PageModel
{
    public IList<Game> Games { get; set; } = [];

    public async Task OnGetAsync()
    {
        Games = (await gameService.GetAllAsync()).Data!;
    }
}