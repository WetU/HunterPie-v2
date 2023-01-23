using HunterPie.Core.Architecture;
using System.Collections.ObjectModel;

namespace HunterPie.UI.Overlay.Widgets.Wirebug.ViewModel;

public class WirebugsViewModel : Bindable
{
    private bool _isWirebugHudHide;

    public ObservableCollection<WirebugViewModel> Elements { get; } = new();
    public bool IsWirebugHudHide { get => _isWirebugHudHide; set => SetValue(ref _isWirebugHudHide, value); }
}
