﻿
using Payhas.Adaptive.ViewModels;

namespace Payhas.Adaptive.Avalonia.Sample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to Avalonia! Updated!";
#pragma warning restore CA1822 // Mark members as static
    }
}
