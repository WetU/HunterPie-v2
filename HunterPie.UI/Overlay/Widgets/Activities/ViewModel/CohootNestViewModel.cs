using HunterPie.Core.Architecture;
using HunterPie.Core.Game.Entity.Environment;
using HunterPie.Core.Game.Enums;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Environment.Activities;

namespace HunterPie.UI.Overlay.Widgets.Activities.ViewModel;

public class CohootNestViewModel : Bindable, IActivity
{
    private int _kamuraCount;
    private int _elgadoCount;
    private int _count;

    public int KamuraCount { get => _kamuraCount; set => SetValue(ref _kamuraCount, value); }
    public int ElgadoCount { get => _elgadoCount; set => SetValue(ref _elgadoCount, value); }
    public int Count { get => _count; set => SetValue(ref _count, value); }
    public int MaxCount = 5;

    public ActivityType Type => ActivityType.Cohoot;
}
