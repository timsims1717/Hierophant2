using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Powers;

public class DonationBoxPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardPlayed(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (!Owner.IsPlayer || cardPlay.Player != Owner.Player 
             || !Hierophant2Card.HasHoard(cardPlay.Card)
             || Hierophant2Card.PlayedHoardCardThisTurn(cardPlay.Card))
        {
            return;
        }
        await CardPileCmd.Draw(choiceContext, Amount, Owner.Player);
        await PlayerCmd.GainEnergy(Amount, Owner.Player);
    }
}