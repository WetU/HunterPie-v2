namespace HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;

[Flags]
public enum WeaponConditions : long
{
    None = 0,
    ShotDamageUp = 1,
    Kijin = 1 << 1,
    Kijin_Jyuu = 1 << 2,
    Kijin_Kyouka = 1 << 3,
    HeavyBowgun_ReduseChargeTime = 1 << 4,
    OverHeatGunLance = 1 << 5,
    MoveWpOffBuffGreatSword = 1 << 6,
    SlashAxeNoUseSlashGauge = 1 << 7,
    RedExtractiveInsectGlaive = 1 << 8,
    WhiteExtractiveInsectGlaive = 1 << 9,
    OrangeExtractiveInsectGlaive = 1 << 10,
    BowWirePowerUp = 1 << 11,
    LanceChargePowerUp = 1 << 12,
    LanceGuardRageS = 1 << 13,
    LanceGuardRageM = 1 << 14,
    LanceGuardRageL = 1 << 15,
    LanceRuten = 1 << 16,
    SlashAxeBottleAwake = 1 << 17,
    LightBowgunWireBuff = 1 << 18,
    ChargeAxeShieldBuff = 1 << 19,
    ChargeAxeSwordBuff = 1 << 20,
    DualBladesSharpnessRecoveryBuff = 1 << 21,
    HammerImpactPullsBuff = 1 << 22,
    ShortSwordOilBuff = 1 << 23,
    HeavyBowgunFullAutoOverHeat = 1 << 24,
    BowArrowUp = 1 << 25,
    GunLanceExplodePileBUff = 1 << 26,
    HornImpactPullsBuff = 1 << 27,
    LongSwordlaiCounterLv2 = 1 << 28,
    LongSwordlaiCounterLv3 = 1 << 29
}