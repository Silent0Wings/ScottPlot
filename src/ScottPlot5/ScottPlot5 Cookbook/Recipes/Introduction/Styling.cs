﻿namespace ScottPlotCookbook.Recipes.Introduction;

public class Styling : ICategory
{
    public string Chapter => "Introduction";
    public string CategoryName => "Styling Plots";
    public string CategoryDescription => "How to customize plots";

    public class StyleClass : RecipeBase
    {
        public override string Name => "Style Helper Functions";
        public override string Description => "Plots contain many objects which can be customized individually " +
            "by assigining to their public properties, but helper methods exist in the Plot's Style object " +
            "that make it easier to customzie many items at once using a simpler API.";

        [Test]
        public override void Execute()
        {
            myPlot.Add.Signal(ScottPlot.Generate.Sin(51));
            myPlot.Add.Signal(ScottPlot.Generate.Cos(51));

            // visible items have public properties that can be customized
            myPlot.XAxis.Label.Text = "Horizontal Axis";
            myPlot.YAxis.Label.Text = "Vertical Axis";
            myPlot.TitlePanel.Label.Text = "Plot Title";

            // the Style object contains helper methods to easily style many items at once
            myPlot.Style.Background(figure: Color.FromHex("#07263b"), data: Color.FromHex("#0b3049"));
            myPlot.Style.ColorAxes(Color.FromHex("#a0acb5"));
            myPlot.Style.ColorGrids(Color.FromHex("#0e3d54"));
        }
    }

    public class AxisCustom : RecipeBase
    {
        public override string Name => "Axis Customization";
        public override string Description => "Axis labels, tick marks, and frame can all be customized.";

        [Test]
        public override void Execute()
        {
            myPlot.Add.Signal(ScottPlot.Generate.Sin(51));
            myPlot.Add.Signal(ScottPlot.Generate.Cos(51));

            myPlot.TitlePanel.Label.Text = "Plot Title";
            myPlot.TitlePanel.Label.Font.Color = Colors.RebeccaPurple;
            myPlot.TitlePanel.Label.Font.Size = 32;
            myPlot.TitlePanel.Label.Font.Name = Fonts.Serif;
            myPlot.TitlePanel.Label.Rotation = -5;
            myPlot.TitlePanel.Label.Font.Bold = false;

            myPlot.YAxis.Label.Text = "Vertical Axis";
            myPlot.YAxis.Label.ForeColor = Colors.Magenta;
            myPlot.YAxis.Label.Italic = true;

            myPlot.XAxis.Label.Text = "Horizontal Axis";
            myPlot.XAxis.Label.Bold = false;
            myPlot.XAxis.Label.FontName = Fonts.Monospace;

            myPlot.XAxis.MajorTickLength = 10;
            myPlot.XAxis.MajorTickWidth = 3;
            myPlot.XAxis.MajorTickColor = Colors.Magenta;
            myPlot.XAxis.MinorTickLength = 5;
            myPlot.XAxis.MinorTickWidth = 0.5f;
            myPlot.XAxis.MinorTickColor = Colors.Green;
            myPlot.XAxis.FrameLineStyle.Color = Colors.LightBlue;
        }
    }

    public class GridCustom : RecipeBase
    {
        public override string Name => "Grid Customization";
        public override string Description => "Grid lines can be customized. " +
            "Custom grid systems can be created to give developers full control of grid rendering, " +
            "but the default grid can be interacted with to customize its appearance.";

        [Test]
        public override void Execute()
        {
            myPlot.Add.Signal(ScottPlot.Generate.Sin(51));
            myPlot.Add.Signal(ScottPlot.Generate.Cos(51));

            ScottPlot.Grids.DefaultGrid grid = myPlot.GetDefaultGrid();

            grid.MajorLineStyle.Color = Colors.Green.WithOpacity(.5);
            grid.MinorLineStyle.Color = Colors.Green.WithOpacity(.1);
            grid.MinorLineStyle.Width = 1;
        }
    }

    public class GridAbove : RecipeBase
    {
        public override string Name => "Grid Above Data";
        public override string Description => "Grid lines are typically drawn beneath " +
            "data, but grids can be configured to render on top of plottables too.";

        [Test]
        public override void Execute()
        {
            var sig = myPlot.Add.Signal(ScottPlot.Generate.Sin(51));
            sig.LineStyle.Width = 10;

            ScottPlot.Grids.DefaultGrid grid = myPlot.GetDefaultGrid();
            grid.MajorLineStyle.Width = 3;
            grid.MajorLineStyle.Color = Colors.WhiteSmoke;
            grid.IsBeneathPlottables = false;
        }
    }

    public class Palette : RecipeBase
    {
        public override string Name => "Palettes";
        public override string Description => "A palette is a set of colors, and the Plot's palette " +
            "defines the default colors to use when adding new plottables. ScottPlot comes with many " +
            "standard palettes, but users may also create their own.";

        [Test]
        public override void Execute()
        {
            myPlot.Palette = new ScottPlot.Palettes.Nord();

            for (int i = 0; i < 5; i++)
            {
                double[] data = ScottPlot.Generate.Sin(100, phase: -i / 20.0f);
                var sig = myPlot.Add.Signal(data);
                sig.LineStyle.Width = 3;
            }
        }
    }

    public class Markers : RecipeBase
    {
        public override string Name => "Markers";
        public override string Description => "Many plot types have a MarkerStyle which can be customized.";

        [Test]
        public override void Execute()
        {
            int count = 21;
            double[] xs = ScottPlot.Generate.Consecutive(count);
            double[] ys = ScottPlot.Generate.Sin(count);

            MarkerShape[] markerShapes = Enum.GetValues<MarkerShape>().ToArray();

            for (int i = 0; i < markerShapes.Length; i++)
            {
                double[] data = ys.Select(y => markerShapes.Length - y + i).ToArray();

                var scatter = myPlot.Add.Scatter(xs, data);

                scatter.Label = markerShapes[i].ToString();

                scatter.MarkerStyle = new MarkerStyle(
                    shape: markerShapes[i],
                    size: 10,
                    color: scatter.LineStyle.Color);
            }

            myPlot.Legend.IsVisible = true;
        }
    }

    public class LineStyles : RecipeBase
    {
        public override string Name => "Line Styles";
        public override string Description => "Many plot types have a LineStyle which can be customized.";

        [Test]
        public override void Execute()
        {
            int count = 21;
            double[] xs = ScottPlot.Generate.Consecutive(count);
            double[] ys = ScottPlot.Generate.Sin(count);

            LinePattern[] linePatterns = Enum.GetValues<LinePattern>().ToArray();

            for (int i = 0; i < linePatterns.Length; i++)
            {
                double[] data = ys.Select(y => linePatterns.Length - y + i).ToArray();

                var scatter = myPlot.Add.Scatter(xs, data);

                scatter.Label = linePatterns[i].ToString();
                scatter.LineStyle.Width = 2;
                scatter.LineStyle.Pattern = linePatterns[i];
                scatter.MarkerStyle = MarkerStyle.None;
            }

            myPlot.Legend.IsVisible = true;
        }
    }

    public class Scaling : RecipeBase
    {
        public override string Name => "Scaling";
        public override string Description => "All components of an image can be scaled up or down in size " +
            "by adjusting the ScaleFactor property. This is very useful for creating images that look nice " +
            "on high DPI displays with display scaling enabled.";

        [Test]
        public override void Execute()
        {
            myPlot.ScaleFactor = 2;
            myPlot.Add.Signal(ScottPlot.Generate.Sin());
            myPlot.Add.Signal(ScottPlot.Generate.Cos());
        }
    }
}
