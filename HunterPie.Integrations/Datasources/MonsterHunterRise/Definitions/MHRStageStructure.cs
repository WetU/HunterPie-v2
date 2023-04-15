using System.Runtime.InteropServices;

namespace HunterPie.Integrations.Datasources.MonsterHunterRise.Definitions;

[StructLayout(LayoutKind.Sequential)]
public struct MHRStageStructure
{
    /**
     * 0 -> None(Main menu)
     * 1 -> Title
     * 2 -> CharMake
     * 3 -> SaveLoad(Char selection)
     * 4 -> Village
     * 5 -> Quest
     * 6 -> LastBoss
     * 7 -> LastBoss_MR
     * 8 -> Arena
     * 9 -> Hyakuryu
     * 10 -> Result
     * 11 -> Demo
     * 12 -> Move(Loading Screen)
     * **/
    public int Type;
    public int VillageSpace;
    public int VillageSpaceChecker;
    public int QuestEnvSpace;
    public int ReverbBaseSpace;
    public int CurrentMapNo;

    public bool IsMakingCharacter() => Type == 2;
    public bool IsSelectingCharacter() => Type == 3;
    public bool IsHuntingZone() => Type is >= 5 and <= 9;
    public bool IsRampage() => Type == 9;
    public bool IsDemo() => Type == 11;
    public bool IsLoadingScreen() => Type == 12;

    public bool IsIrrelevantStage() => IsMakingCharacter() || IsSelectingCharacter() || IsDemo() || IsLoadingScreen();
}
