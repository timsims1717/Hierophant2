using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Powers;

public class DazzlePower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!cardPlay.Card.Tags.Contains(Hierophant2Card.IsDoubloon)
            || cardPlay.Card.Owner != Owner.Player)
        {
            return;
        }

        var strAmt = Amount;
        if (strAmt == 0)
        {
            return;
        }
        Creature? target = Owner.Player.RunState.Rng.CombatTargets.NextItem(CombatState.Enemies);
        if (target == null)
        {
            return;
        }
        Flash();
        await PowerCmd.Apply<DazzledPower>(choiceContext, target, strAmt, Owner, null);
    }
}