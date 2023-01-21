using HunterPie.Core.Client;
using HunterPie.Core.Client.Configuration.Overlay;
using HunterPie.Core.Game.Entity.Game;
using HunterPie.Integrations.Datasources.MonsterHunterRise;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Player;
using HunterPie.UI.Overlay.Widgets.Wirebug.ViewModel;
using HunterPie.UI.Overlay.Widgets.Wirebug.Views;

namespace HunterPie.UI.Overlay.Widgets.Wirebug;

public class WirebugWidgetContextHandler : IContextHandler
{
    private readonly MHRContext _context;
    private readonly WirebugsViewModel _viewModel;
    private readonly WirebugsView _view;
    private MHRPlayer Player => (MHRPlayer)_context.Game.Player;

    public WirebugWidgetContextHandler(MHRContext context)
    {
        _view = new WirebugsView(ClientConfig.Config.Rise.Overlay.WirebugWidget);
        _ = WidgetManager.Register<WirebugsView, WirebugWidgetConfig>(_view);

        _viewModel = _view.ViewModel;
        _context = context;

        HookEvents();
        UpdateData();
    }

    public void HookEvents()
    {
        Player.OnWirebugsRefresh += OnWirebugsRefresh;
        _context.Game.OnHudStateChange += OnHudStateChange;
    }

    public void UnhookEvents()
    {
        Player.OnWirebugsRefresh -= OnWirebugsRefresh;
        _context.Game.OnHudStateChange -= OnHudStateChange;

        foreach (WirebugViewModel vm in _viewModel.Elements)
            if (vm is WirebugContextHandler model)
                model.UnhookEvents();

        _viewModel.Elements.Clear();

        _ = WidgetManager.Unregister<WirebugsView, WirebugWidgetConfig>(_view);
    }

    private void OnHudStateChange(object sender, IGame e) => _viewModel.IsWirebugHudOpen = e.IsWirebugHudOpen;

    private void OnWirebugsRefresh(object sender, MHRWirebug[] e)
    {
        _view.Dispatcher.Invoke(() =>
        {
            foreach (WirebugViewModel vm in _viewModel.Elements)
                if (vm is WirebugContextHandler model)
                    model.UnhookEvents();

            _viewModel.Elements.Clear();

            foreach (MHRWirebug wirebug in e)
                _viewModel.Elements.Add(new WirebugContextHandler(wirebug));
        });
    }

    private void UpdateData()
    {
        _viewModel.IsWirebugHudOpen = _context.Game.IsWirebugHudOpen;

        foreach (MHRWirebug wirebug in Player.Wirebugs)
            _viewModel.Elements.Add(new WirebugContextHandler(wirebug));
    }
}
