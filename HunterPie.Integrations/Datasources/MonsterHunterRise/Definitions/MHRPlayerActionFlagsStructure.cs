using System.Runtime.InteropServices;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;

namespace HunterPie.Integrations.Datasources.MonsterHunterRise.Definitions;

[StructLayout(LayoutKind.Explicit)]
public struct MHRPlayerActionFlagStructure
{
    [FieldOffset(0x20)]
    public ActionFlags ActionFlag;
}
