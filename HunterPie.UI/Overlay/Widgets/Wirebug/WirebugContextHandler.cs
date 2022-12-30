using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Player;
using HunterPie.UI.Overlay.Widgets.Wirebug.ViewModel;
using System;

namespace HunterPie.UI.Overlay.Widgets.Wirebug;

internal class WirebugContextHandler : WirebugViewModel, IContextHandler
{
    public readonly MHRWirebug Context;
    public readonly MHRPlayer Player;

    public WirebugContextHandler(MHRWirebug context, MHRPlayer player)
    {
        Context = context;
        Player = player;
        
        UpdateData();
        HookEvents();
    }

    public void HookEvents()
    {
        Context.OnCooldownUpdate += OnCooldownUpdate;
        Context.OnTimerUpdate += OnTimerUpdate;
        Context.OnAvailable += OnAvailable;
        Context.OnBlockedStateChange += OnBlockedStateChange;
        Player.OnPlayerRideOn += OnPlayerRideOn;
    }

    public void UnhookEvents()
    {
        Context.OnCooldownUpdate -= OnCooldownUpdate;
        Context.OnTimerUpdate -= OnTimerUpdate;
        Context.OnAvailable -= OnAvailable;
        Context.OnBlockedStateChange -= OnBlockedStateChange;
        Player.OnPlayerRideOn -= OnPlayerRideOn;
    }

    private void OnBlockedStateChange(object sender, MHRWirebug e) => IsBlocked = e.IsBlocked;

    private void OnTimerUpdate(object sender, MHRWirebug e)
    {
        MaxTimer = e.MaxTimer;
        Timer = e.Timer;
        IsTemporary = Context.Timer > 0;
    }

    private void OnAvailable(object sender, MHRWirebug e) => IsAvailable = e.IsAvailable;

    private void OnCooldownUpdate(object sender, MHRWirebug e)
    {
        MaxCooldown = e.MaxCooldown;
        Cooldown = e.Cooldown;
        OnCooldown = Cooldown > 0;
    }

    private void OnPlayerRideOn(object sender, EventArgs e) => IsMarionette = Player.IsMarionette;

    private void UpdateData()
    {
        IsBlocked = Context.IsBlocked;
        MaxCooldown = Context.MaxCooldown == 0 ? 400 : Context.MaxCooldown;
        Cooldown = Context.Cooldown;
        IsAvailable = Context.IsAvailable;

        MaxTimer = Context.MaxTimer;
        Timer = Context.Timer;
        IsTemporary = Context.Timer > 0;
        IsMarionette= Player.IsMarionette;
    }
}
