using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class LocustPlagueUpgradedPower() : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterSideTurnStart(CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        await Trigger(side);
    }

    public override async Task BeforeSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        await Trigger(side);
    }

    private async Task Trigger(CombatSide side)
    {
        if (side != Owner.Side || !Owner.IsPlayer)
        {
            return;
        }

        Flash();
        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(),
            CombatState.HittableEnemies, 
            Hierophant2Card.TotalCostOfCardsInHandOwner(Owner.Player, null) * Amount, 
            ValueProp.Unpowered,
            Owner);
    }
}