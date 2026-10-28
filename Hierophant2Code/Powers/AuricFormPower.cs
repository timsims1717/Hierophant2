using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Powers;

public class AuricFormPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side == Owner.Side && Owner.IsPlayer)
        {
            Flash();
            PlayerChoiceContext ctx = new ThrowingPlayerChoiceContext();
            decimal loseGold = Owner.Player.Gold * Amount * 0.01M;
            await CreatureCmd.GainBlock(Owner, loseGold, DynamicVars.CalculatedBlock.Props, null);
            await PowerCmd.Apply<FervorPower>(ctx, 
                Owner,
                loseGold, 
                Owner, null);
            await PowerCmd.Apply<PietyPower>(ctx, 
                Owner,
                loseGold, 
                Owner, null);
            await PlayerCmd.LoseGold(loseGold, Owner.Player);
        }
    }
}