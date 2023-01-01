using System;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Player;
using HunterPie.UI.Overlay.Widgets.Wirebug.ViewModel;

namespace HunterPie.UI.Overlay.Widgets.Wirebug;

internal class WirebugContextHandler : WirebugViewModel, IContextHandler
{
    private ulong _commonCondition = 0;
    private ulong _debuffCondition = 0;

    private readonly ulong windmantle = (ulong)CommonCondition.WindMantle;
    private readonly ulong gold = (ulong)CommonCondition.MarionetteTypeGold;
    private readonly ulong ruby = (ulong)CommonCondition.MarionetteTypeRuby;
    private readonly ulong iceL = (ulong)DebuffCondition.IceL;

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
        Context.OnCommonConditionChange += OnCommonConditionChange;
        Context.OnDebuffConditionChange += OnDebuffConditionChange;
    }

    public void UnhookEvents()
    {
        Context.OnCooldownUpdate -= OnCooldownUpdate;
        Context.OnTimerUpdate -= OnTimerUpdate;
        Context.OnAvailable -= OnAvailable;
        Context.OnBlockedStateChange -= OnBlockedStateChange;
        Context.OnCommonConditionChange -= OnCommonConditionChange;
        Context.OnDebuffConditionChange -= OnDebuffConditionChange;
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

    private void OnCommonConditionChange(object sender, MHRWirebug e)
    {
        _commonCondition = e.CommonCondition;

        if (_commonCondition == 0 && _debuffCondition == 0)
        {
            PlayerCondition = 0;
            return;
        }

        ulong result;

        if (_commonCondition != 0)
        {
            result = _commonCondition & windmantle;

            if (result == windmantle)
            {
                PlayerCondition = 1;
                return;
            }

            result = _commonCondition & gold;
            
            if (result == gold)
            {
                PlayerCondition = 2;
                return;
            }

            result = _commonCondition & ruby;

            if (result == ruby)
            {
                PlayerCondition = 3;
                return;
            }

            if (PlayerCondition > 0 && PlayerCondition < 4)
            {
                return;
            }

            if (_debuffCondition != 0)
            {
                result = _debuffCondition & iceL;

                if (result == iceL)
                {
                    PlayerCondition = 4;
                    return;
                }
            }
        }
        
        PlayerCondition = 0;
    }

    private void OnDebuffConditionChange(object sender, MHRWirebug e)
    {
        _debuffCondition = e.DebuffCondition;

        if (_commonCondition == 0 && _debuffCondition == 0)
        {
            PlayerCondition = 0;
            return;
        }

        if (PlayerCondition > 0 && PlayerCondition < 4)
        {
            return;
        }

        if (_debuffCondition != 0)
        {
            ulong result = _debuffCondition & iceL;

            if (result == iceL)
            {
                PlayerCondition = 4;
                return;
            }
        }

        PlayerCondition = 0;
    }

    private void UpdateData()
    {
        IsBlocked = Context.IsBlocked;
        MaxCooldown = Context.MaxCooldown == 0 ? 400 : Context.MaxCooldown;
        Cooldown = Context.Cooldown;
        IsAvailable = Context.IsAvailable;

        MaxTimer = Context.MaxTimer;
        Timer = Context.Timer;
        IsTemporary = Context.Timer > 0;

        if (Context.CommonCondition == 0 && Context.DebuffCondition == 0)
        {
            PlayerCondition = 0;
            return;
        }

        ulong result;

        if (Context.CommonCondition != 0)
        {
            result = Context.CommonCondition & windmantle;

            if (result == windmantle)
            {
                PlayerCondition = 1;
                return;
            }

            result = Context.CommonCondition & gold;

            if (result == gold)
            {
                PlayerCondition = 2;
                return;
            }

            result = Context.CommonCondition & ruby;

            if (result == ruby)
            {
                PlayerCondition = 3;
                return;
            }

            if (PlayerCondition > 0 && PlayerCondition < 4)
            {
                return;
            }

            if (Context.DebuffCondition != 0)
            {
                result = Context.DebuffCondition & iceL;

                if (result == iceL)
                {
                    PlayerCondition = 4;
                    return;
                }
            }
        }

        PlayerCondition = 0;
    }
}
