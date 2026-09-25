using AppKiller.ViewModels;
using AppKiller.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommandLine;
using System;

namespace AppKiller;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var result = Parser.Default.ParseArguments<Options>(desktop.Args);

            if (result is Parsed<Options>)
            {
                desktop.MainWindow = new MainWindow(new MainViewModel(result.Value.WaitingTime, result.Value.Applications));
            }
            else if (result is NotParsed<Options>)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.ToString());
                }

                Environment.Exit(1);
            }
        }

        base.OnFrameworkInitializationCompleted();
    }
}