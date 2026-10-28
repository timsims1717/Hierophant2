using MegaCrit.Sts2.Core.Entities.Powers;

namespace Hierophant2.Hierophant2Code.Powers;

public class HolySoulPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
}