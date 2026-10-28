using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class FellowshipPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    // public override int DisplayAmount => DynamicVars["BlockAmount"].IntValue;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        new List<DynamicVar> { new("BlockAmount", 8) };
    
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override async Task AfterSideTurnStart(CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (side == CombatSide.Player)
        {
            Flash();
            await CreatureCmd.GainBlock(Owner, DynamicVars["BlockAmount"].BaseValue, ValueProp.Move, null);
            await PowerCmd.Decrement(this);
        }
    }
}