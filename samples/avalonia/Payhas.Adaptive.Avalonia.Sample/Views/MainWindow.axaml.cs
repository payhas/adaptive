using Avalonia.Styling;
using Payhas.Adaptive.Controls;
using ReactiveUI.SourceGenerators;

namespace Payhas.Adaptive.Avalonia.Sample.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [ReactiveCommand]
        private void ToggleTheme()
        {
            RequestedThemeVariant = ActualThemeVariant.Key.ToString() switch
            {
                nameof(ThemeVariant.Light) => ThemeVariant.Dark,
                nameof(ThemeVariant.Dark) => ThemeVariant.Light,
                _ => ThemeVariant.Default,
            };
        }
    }
}
