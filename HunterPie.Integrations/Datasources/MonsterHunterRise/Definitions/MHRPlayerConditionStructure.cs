using System.Runtime.InteropServices;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;

namespace HunterPie.Integrations.Datasources.MonsterHunterRise.Definitions;

[StructLayout(LayoutKind.Explicit)]
public struct MHRPlayerConditionStructure
{
    [FieldOffset(0x10)]
    public CommonConditions CommonCondition;

    [FieldOffset(0x28)]
    public WeaponConditions WeaponCondition;

    [FieldOffset(0x38)]
    public DebuffConditions DebuffCondition;
}
