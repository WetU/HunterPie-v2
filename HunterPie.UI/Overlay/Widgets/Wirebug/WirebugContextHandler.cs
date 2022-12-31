using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Player;
using HunterPie.UI.Overlay.Widgets.Wirebug.ViewModel;
using System;

namespace HunterPie.UI.Overlay.Widgets.Wirebug;

internal class WirebugContextHandler : WirebugViewModel, IContextHandler
{
    public readonly MHRWirebug Context;

    public WirebugContextHandler(MHRWirebug context)
    {
        Context = context;
        
        UpdateData();
        HookEvents();
    }

    public void HookEvents()
    {
        Context.OnCooldownUpdate += OnCooldownUpdate;
        Context.OnTimerUpdate += OnTimerUpdate;
        Context.OnAvailable += OnAvailable;
        Context.OnBlockedStateChange += OnBlockedStateChange;
        Context.OnMarionetteStateChange += OnMarionetteStateChange;
    }

    public void UnhookEvents()
    {
        Context.OnCooldownUpdate -= OnCooldownUpdate;
        Context.OnTimerUpdate -= OnTimerUpdate;
        Context.OnAvailable -= OnAvailable;
        Context.OnBlockedStateChange -= OnBlockedStateChange;
        Context.OnMarionetteStateChange -= OnMarionetteStateChange;
    }

    private void OnBlockedStateChange(object sender, MHRWirebug e) => IsBlocked = e.IsBlocked;

    private void OnTimerUpdate(object sender, MHRWirebug e)
    {
        MaxTimer = e.MaxTimer;
        Timer = e.Timer;
        IsTemporary = Context.Timer > 0;
        if (e.IsMarionette)
        {
            if (IsTemporary)
            {
                MarionetteAndTemporary = true;
                IsMarionette = false;
            }
            else
            {
                MarionetteAndTemporary = false;
                IsMarionette = true;
            }
        }
    }

    private void OnAvailable(object sender, MHRWirebug e) => IsAvailable = e.IsAvailable;

    private void OnCooldownUpdate(object sender, MHRWirebug e)
    {
        MaxCooldown = e.MaxCooldown;
        Cooldown = e.Cooldown;
        OnCooldown = Cooldown > 0;
    }

    private void OnMarionetteStateChange(object sender, MHRWirebug e)
    {
        IsMarionette = e.IsMarionette;
        if (IsMarionette)
        {
            if (IsTemporary)
            {
                MarionetteAndTemporary = true;
                IsMarionette = false;
            }
            else
            {
                MarionetteAndTemporary = false;
                IsMarionette = true;
            }
        }
    }

    private void UpdateData()
    {
        IsBlocked = Context.IsBlocked;
        IsMarionette = Context.IsMarionette;
        MaxCooldown = Context.MaxCooldown == 0 ? 400 : Context.MaxCooldown;
        Cooldown = Context.Cooldown;
        IsAvailable = Context.IsAvailable;

        MaxTimer = Context.MaxTimer;
        Timer = Context.Timer;
        IsTemporary = Context.Timer > 0;

        if (IsMarionette)
        {
            if (IsTemporary)
            {
                MarionetteAndTemporary = true;
                IsMarionette = false;
            }
            else
            {
                MarionetteAndTemporary = false;
                IsMarionette = true;
            }
        }
    }
}
