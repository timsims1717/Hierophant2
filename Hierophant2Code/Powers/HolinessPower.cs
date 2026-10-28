using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Powers;

public class HolinessPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.IsPlayer && cardPlay.Player == Owner.Player
            && cardPlay.Card.Tags.Contains(Hierophant2Card.IsHoly))
        {
            Flash();
            await PowerCmd.Apply<HolyStrengthPower>(choiceContext, Owner, Amount, Owner, null);
            await PowerCmd.Apply<HolyDexterityPower>(choiceContext, Owner, Amount, Owner, null);
        }
    }
}