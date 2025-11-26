using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Areas.Admin.Views.Games;

public class CreateModel(IGameService gameService) : PageModel
{
    [BindProperty] public Game Game { get; set; } = default!;

    [BindProperty] public IFormFile? ImageFile { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await gameService.CreateAsync(Game, ImageFile);

        return RedirectToPage("./Index");
    }
}