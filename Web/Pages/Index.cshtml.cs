using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private IReadOnlyRepo<Part> Parts { get; }

    public IndexModel(IReadOnlyRepo<Part> parts)
    {
        this.Parts = parts;
    }

    public IEnumerable<Part> AllParts { get; set; } = Enumerable.Empty<Part>();

    public void OnGet()
    {
        this.AllParts = this.Parts.GetAll().ToList();
    }
}