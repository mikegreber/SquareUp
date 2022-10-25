namespace SquareUp.Library;

public static class Converters
{
    public static LinearGradientBrush ConvertBackground(string s)
    {
        if (string.IsNullOrEmpty(s)) 
            return new LinearGradientBrush(new GradientStopCollection() { new(Colors.Red, 0.0f) }, new Point(0, 0), new Point(1, 1));

        var stops = new GradientStopCollection();
        var colors = s.Split(":");

        for (var i = 0; i < colors.Length; i++)
        {
            var color = colors[i];
            stops.Add(new GradientStop(Color.FromArgb(color), (float)i / colors.Length));
        }

        return new LinearGradientBrush(stops, new Point(0, 0), new Point(1, 1));
    }
}