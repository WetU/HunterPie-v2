using HunterPie.Core.Architecture;
using System.Collections.ObjectModel;

namespace HunterPie.UI.Overlay.Widgets.Wirebug.ViewModel;

public class WirebugsViewModel : Bindable
{
    private bool _isWirebugHudOpen;

    public ObservableCollection<WirebugViewModel> Elements { get; } = new();
    public bool IsWirebugHudOpen { get => _isWirebugHudOpen; set => SetValue(ref _isWirebugHudOpen, value); }
}
