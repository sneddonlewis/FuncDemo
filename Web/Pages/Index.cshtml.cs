using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Media;
using Models.Types;
using Models.Types.Media;

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

    public BarcodeGeneration.Margins Margins => new(Horizontal: 5, Vertical: 3, BarHeight: 25);

    public BarcodeGeneration.Style Style =>
        new(ThinBarWidth: 1.5f, ThickBarWidth: 4, GapWidth: 2, Padding: 2, AntiAlias: true);
}