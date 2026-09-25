using AppKiller.ViewModels;
using FluentAvalonia.UI.Windowing;

namespace AppKiller.Views;

public partial class MainWindow : FAAppWindow
{
    public MainViewModel ViewModel { get; }

    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = viewModel;
        
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        await ViewModel.KillApplications();
    }
}