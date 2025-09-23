using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UI.Areas.Admin.Views.Games
{
    public class EditModel(IGameService gameService) : PageModel
    {
        [BindProperty]
        public Game Game { get; set; } = default!;
        
        [BindProperty]
        public IFormFile? ImageFile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var gameGetResponse = await gameService.GetByIdAsync(id.Value);

            if (!gameGetResponse.Successful)
            {
                return NotFound(gameGetResponse.ErrorMessage ?? string.Empty);
            }

            Game = gameGetResponse.Data!;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await gameService.UpdateAsync(Game, ImageFile);

            return RedirectToPage("./Index");
        }
    }
}
