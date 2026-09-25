using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AppKiller.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool isBusy = false;

    [ObservableProperty]
    private string busyText = string.Empty;

    [ObservableProperty]
    private int waitingTime;

    public ObservableCollection<string> ApplicationNames = new ObservableCollection<string>();

    public MainViewModel(int waitingTime, IEnumerable<string> appNames)
    {
        WaitingTime = waitingTime;
        ApplicationNames.Clear();

        foreach (var name in appNames)
        {
            ApplicationNames.Add(name);
        }
    }

    public async Task KillApplications()
    {
        var processes = new List<Process>();

        do
        {
            processes.Clear();
            foreach (var name in ApplicationNames)
            {
                processes.AddRange(Process.GetProcessesByName(name));
            }

            foreach (var process in processes)
            {
                BusyText = $"Schließe {process.ProcessName} (ID: {process.Id})...";
                process.Kill();
            }

            BusyText = $"Warte {WaitingTime} Sekunden, ob weitere Prozesse in der Zwischenzeit gestartet wurden...";
            await Task.Delay(WaitingTime * 1000);
        } while (processes.Count > 0);

        BusyText = "Es scheint kein Prozess mehr zu laufen. Das Programm kann jetzt beendet werden.";
        IsBusy = false;
    }
}
