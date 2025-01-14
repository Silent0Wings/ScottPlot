﻿namespace ScottPlot.Plottables;

public class LinePlot : IPlottable
{
    public Coordinates Start { get; set; }
    public Coordinates End { get; set; }
    public LineStyle LineStyle { get; set; } = new();
    public MarkerStyle MarkerStyle { get; set; } = new() { Size = 0 };
    public string Label { get; set; } = string.Empty;
    public bool IsVisible { get; set; } = true;
    public IAxes Axes { get; set; } = new Axes();
    public IEnumerable<LegendItem> LegendItems => LegendItem.Single(Label, LineStyle, MarkerStyle);

    public AxisLimits GetAxisLimits()
    {
        CoordinateRect boundingRect = new(Start, End);
        return new AxisLimits(boundingRect);
    }

    public void Render(RenderPack rp)
    {
        using SKPaint paint = new();
        LineStyle.ApplyToPaint(paint);

        using SKPath path = new();
        path.MoveTo(Axes.GetPixel(Start).ToSKPoint());
        path.LineTo(Axes.GetPixel(End).ToSKPoint());

        MarkerStyle.Render(rp.Canvas, Axes.GetPixel(Start));
        MarkerStyle.Render(rp.Canvas, Axes.GetPixel(End));

        rp.Canvas.DrawPath(path, paint);
    }
}
