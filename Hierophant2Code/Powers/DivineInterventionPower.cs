using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Powers;

public class DivineInterventionPower() : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override bool ShouldDieLate(Creature creature)
    {
        return creature != Owner;
    }

    public override async Task AfterPreventingDeath(Creature creature)
    {
        Flash();
        await CreatureCmd.Heal(creature, Math.Max(1M, creature.MaxHp * (Amount / 100M)));
        await PowerCmd.Remove(this);
    }

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (side == Owner.Side)
        {
            await PowerCmd.Remove(this);
        }
    }
}