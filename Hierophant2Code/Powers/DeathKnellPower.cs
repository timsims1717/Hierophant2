using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Powers;

public class DeathKnellPower() : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        new List<DynamicVar> { new PowerVar<FervorPower>(40) };

    public override async Task AfterSideTurnStart(CombatSide side,
        IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side == Owner.Side)
        {
            if (Amount == 1)
            {
                Flash();
                PlayerChoiceContext ctx = new ThrowingPlayerChoiceContext();
                await PowerCmd.Apply<FervorPower>(ctx, Owner, DynamicVars.Power<FervorPower>().IntValue, Owner, null);
                await PowerCmd.Remove(this);
            }
            else
            {
                await PowerCmd.Decrement(this);
            }
        }
    }
}