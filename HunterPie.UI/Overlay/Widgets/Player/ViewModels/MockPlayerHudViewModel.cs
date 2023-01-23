using HunterPie.Core.Game.Enums;
using HunterPie.UI.Architecture.Test;
using WeaponId = HunterPie.Core.Game.Enums.Weapon;

namespace HunterPie.UI.Overlay.Widgets.Player.ViewModels;

public class MockPlayerHudViewModel : PlayerHudViewModel
{

    public MockPlayerHudViewModel()
    {
        SetupMocks();
        SetupSharpnessMocks();
    }

    private void SetupMocks()
    {
        Name = "Lyss";
        Level = 99;
        Health = 180.0;
        MaxHealth = 180.0;
        MaxExtendableHealth = 200.0;
        RecoverableHealth = 0;
        Stamina = 2000.0;
        MaxStamina = 2500.0;
        MaxPossibleStamina = 2500.0;
        Weapon = WeaponId.SwitchAxe;
<<<<<<< HEAD
=======
        IsPlayerHudHide = false;
>>>>>>> 8a854c465d57939c679700374647777d50429afc
    }

    private void SetupSharpnessMocks()
    {

        SharpnessViewModel.Sharpness = 50;
        SharpnessViewModel.MaxSharpness = 50;

        MockBehavior.Run(() => SharpnessViewModel.SharpnessLevel = (Sharpness)(((int)SharpnessViewModel.SharpnessLevel + 1) % 7), 2);
    }
}
