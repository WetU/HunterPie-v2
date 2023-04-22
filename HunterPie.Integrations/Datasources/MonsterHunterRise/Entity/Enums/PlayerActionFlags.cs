namespace HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;

[Flags]
public enum PrimaryActionFlags : long
{
    None = 0,
    WireTargetVerticalRotate = 1,
    ItemPresentAction = 1 << 1,
    ItemThankAction = 1 << 2,
    SystemNoDeath = 1 << 3,
    IsQuickMatchHelp = 1 << 4,
    MaximumMight = 1 << 5,
    IsStartBatto = 1 << 6,
    BattoPowerUsed = 1 << 7,
    IsAttackCritical = 1 << 8,
    HeavyDie = 1 << 9,
    IsMindsEye = 1 << 10,
    IsAirouMatatabi = 1 << 11,
    ModeQuestResult = 1 << 12,
    IsHornWallHyperArmor = 1 << 13,
    CookingNow = 1 << 14,
    CookingSuccess = 1 << 15,
    CookingFailed = 1 << 16,
    WeaponDispOn = 1 << 17,
    IsFaceMotionOnState = 1 << 18,
    WeaponDispOff = 1 << 19,
    WireHoldOn = 1 << 20,
    Squat = 1 << 21,
    SittingTableOrBench = 1 << 22,
    InTent = 1 << 23,
    RequestIgnoreVerticalRiseAdjust = 1 << 24,
    GuestFirstPacket = 1 << 25,
    OverrideMotionMode = 1 << 26,
    WireJumpUpActionEnd = 1 << 27,
    ItemReturnCampWait = 1 << 28,
    GuestResetAllHitAttack = 1 << 29,
    Fishing = 1 << 30,
    ChangeVillage = 1 << 31
}

[Flags]
public enum SecondaryActionFlags : uint
{
    None = 0,
    RedirectionUnused = 1,
    UseLobbyUpperBodyAdjust = 1 << 1,
    PowderMantle_TriggerAttack = 1 << 2,
    PowderMantle_TriggerDamage = 1 << 3,
    Strife_Small = 1 << 4,
    Strife_Large = 1 << 5,
    HeavenSent_Check = 1 << 6,
    HeavenSent_EnableDist = 1 << 7
}