using Models.Common;
using Models.Types;
using Models.Types.Media;
using SkiaSharp;

namespace Models.Media;

public static class BarcodeGeneration
{
    public record Margins(float Horizontal, float Vertical, float BarHeight);

    public record Style(float ThinBarWidth, float ThickBarWidth, float GapWidth, float Padding, bool AntiAlias);
    
    public static FileContent ToCode39(this StockKeepingUnit sku, Margins margins, Style style) =>
        sku.Value.ToCode39Bars().ToCode39Bitmap(margins, style).ToPng();

    private static SKBitmap ToCode39Bitmap(this IEnumerable<int> bars, Margins margins, Style style) =>
        bars.ToGraphicalLines(style).ToBarcodeBitmap(margins, style);

    private static SKPaint[] ToGraphicalLines(this IEnumerable<int> bars, Style style) =>
        bars.ToGraphicalLines(Gap(style), ThinBar(style), ThickBar(style));

    private static SKPaint[] ToGraphicalLines(this IEnumerable<int> bars, params SKPaint[] lines) =>
        bars.Select(bar => lines[bar]).ToArray();

    private static SKPaint ThickBar(Style style) => Bar(SKColors.Black, style.ThickBarWidth);
    private static SKPaint ThinBar(Style style) => Bar(SKColors.Black, style.ThinBarWidth);
    private static SKPaint Gap(Style style) => Bar(SKColors.Transparent, style.GapWidth);

    private static SKPaint Bar(SKColor color, float thickness) => new SKPaint
    {
        Color = color,
        Style = SKPaintStyle.Stroke,
        StrokeCap = SKStrokeCap.Butt,
        StrokeWidth = thickness,
        IsAntialias = true,
    };

    private static SKBitmap ToBarcodeBitmap(this SKPaint[] bars, Margins margins, Style style)
    {
        float horizontalMargin = margins.Horizontal;
        float verticalMargin = margins.Vertical;
        float barHeight = margins.BarHeight;
        
        float padding = style.Padding;
        
        float barsWidth = bars.Sum(bar => bar.StrokeWidth);
        float height = barHeight + 2 * verticalMargin;
        float width = barsWidth + (bars.Length - 1) * padding + 2 * horizontalMargin;

        SKBitmap bitmap = InitializeBitmap(width, height);
        SKCanvas canvas = new(bitmap);

        float offset = horizontalMargin;
        foreach (SKPaint bar in bars)
        {
            float x = offset + bar.StrokeWidth / 2;
            canvas.DrawLine(x, verticalMargin, x, barHeight + verticalMargin, bar);
            offset += bar.StrokeWidth + padding;
        }

        return bitmap;
    }

    private static SKBitmap InitializeBitmap(float width, float height)
    {
        SKBitmap bitmap = new ((int)Math.Ceiling(width), (int)Math.Ceiling(height));
        SKCanvas canvas = new (bitmap);
        canvas.Clear(SKColors.White);
        return bitmap;
    }
}
