using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Globalization;

namespace Payhas.Adaptive.Converters;

public class UriToBitmapConverter : IValueConverter
{
    public Uri? BaseUri { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is Uri uri && uri != null
            ? new Bitmap(AssetLoader.Open(uri, BaseUri))
            : null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
