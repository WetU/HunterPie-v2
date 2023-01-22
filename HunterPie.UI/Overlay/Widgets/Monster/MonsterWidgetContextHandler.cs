using HunterPie.Core.Client;
using HunterPie.Core.Client.Configuration;
using HunterPie.Core.Client.Configuration.Overlay;
using HunterPie.Core.Game;
using HunterPie.Core.Game.Entity.Enemy;
using HunterPie.Core.Game.Entity.Game;
using HunterPie.Core.System;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Game;
using HunterPie.UI.Overlay.Widgets.Monster.ViewModels;
using HunterPie.UI.Overlay.Widgets.Monster.Views;
using System;
using System.Linq;

namespace HunterPie.UI.Overlay.Widgets.Monster;

public class MonsterWidgetContextHandler : IContextHandler
{
    private readonly MonstersViewModel _viewModel;
    private readonly MonstersView _view;
    private MonsterWidgetConfig Settings => _view.Settings;
    private readonly IContext _context;
    private IGame Game => _context.Game;
    private MHRGame MHRGame => (MHRGame)Game;

    public MonsterWidgetContextHandler(IContext context)
    {
        OverlayConfig config = ClientConfigHelper.GetOverlayConfigFrom(ProcessManager.Game);

        _view = new MonstersView(config.BossesWidget);
        _ = WidgetManager.Register<MonstersView, MonsterWidgetConfig>(_view);

        _viewModel = _view.ViewModel;
        _context = context;

        UpdateData();
        HookEvents();
    }

    private void UpdateData()
    {
        _viewModel.IsTgCameraHide = MHRGame.IsTgCameraHide;

        foreach (IMonster monster in Game.Monsters)
        {
            monster.OnTargetChange += OnTargetChange;
            _viewModel.Monsters.Add(new MonsterContextHandler(monster, Settings));
        }

        CalculateVisibleMonsters();
    }

    public void HookEvents()
    {
        Game.OnMonsterSpawn += OnMonsterSpawn;
        Game.OnMonsterDespawn += OnMonsterDespawn;
        MHRGame.OnRiseHudStateChange += OnRiseHudStateChange;
    }

    public void UnhookEvents()
    {
        Game.OnMonsterSpawn -= OnMonsterSpawn;
        Game.OnMonsterDespawn -= OnMonsterDespawn;
        MHRGame.OnRiseHudStateChange -= OnRiseHudStateChange;

        _view.Dispatcher.Invoke(() =>
        {
            foreach (MonsterContextHandler ctxHandler in _viewModel.Monsters.Cast<MonsterContextHandler>())
                ctxHandler.Dispose();

            _viewModel.Monsters.Clear();
        });

        _ = WidgetManager.Unregister<MonstersView, MonsterWidgetConfig>(_view);
    }

    private void OnMonsterDespawn(object sender, IMonster e)
    {
        _view.Dispatcher.Invoke(() =>
        {
            MonsterContextHandler monster = _viewModel.Monsters
                .Cast<MonsterContextHandler>()
                .FirstOrDefault(handler => handler.Context == e);

            if (monster is null)
                return;

            monster.Dispose();

            _ = _viewModel.Monsters.Remove(monster);
        });

        e.OnTargetChange -= OnTargetChange;
        CalculateVisibleMonsters();
    }

    private void OnMonsterSpawn(object sender, IMonster e)
    {
        _view.Dispatcher.Invoke(() => _viewModel.Monsters.Add(new MonsterContextHandler(e, Settings)));

        e.OnTargetChange += OnTargetChange;
        CalculateVisibleMonsters();
    }

    private void OnTargetChange(object sender, EventArgs e) => CalculateVisibleMonsters();

    private void OnRiseHudStateChange(object sender, MHRGame e) => _viewModel.IsTgCameraHide = e.IsTgCameraHide;

    private void CalculateVisibleMonsters()
    {
        int targets = Game.Monsters.Count(m => m.IsTarget);

        _viewModel.VisibleMonsters = Settings.ShowOnlyTarget.Value switch
        {
            true => targets,
            false => targets == 0 ? Game.Monsters.Count : targets,
        };
        _viewModel.MonstersCount = Game.Monsters.Count;
    }
}
