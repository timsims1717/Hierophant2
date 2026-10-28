using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Powers;

public class RepentancePower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext,
        CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player &&
            !CombatManager.Instance.IsOverOrEnding &&
            Owner.HasPower<PietyPower>())
        {
            Flash();
            PowerCmd.Apply<PietyPower>(choiceContext,
                Owner,
                Owner.GetPowerAmount<PietyPower>(),
                Owner,
                null);
        }
    }

    public override async Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player && !CombatManager.Instance.IsOverOrEnding)
        {
            await PowerCmd.Decrement(this);
        }
    }
}