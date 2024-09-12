using Models.Types;
using Models.Types.Media;

namespace Models.Media;

public static class BarcodeGeneration
{
    public static FileContent ToCode39(this StockKeepingUnit sku, int barHeight)
    {
        float horizontalMargin = 5;
        float verticalMargin = 3;
        float padding = 2.0f;

        // SKPaint[] bars = new[]
        // {
        //     ThinBar, Space, ThinBar, ThickBar, ThickBar, ThinBar,
        // };
        //
        // int height = (int)Math.Ceiling(barHeight + verticalMargin * 2);
        // int width = (int)Math.Ceiling(bars.Sum(bar => bar.StrokeWidth) + (bars.Length - 1) * padding + horizontalMargin * 2);
        return new FileContent([], "image/png");
    }
}