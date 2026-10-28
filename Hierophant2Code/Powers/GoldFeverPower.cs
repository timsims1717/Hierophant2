using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class GoldFeverPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!cardPlay.Card.Tags.Contains(Hierophant2Card.IsDoubloon)
            || !Applier.IsPlayer
            || cardPlay.Card.Owner != Applier.Player)
        {
            return;
        }

        Flash();
        await CreatureCmd.Damage(choiceContext,
            Owner,
            Amount, 
            ValueProp.Unpowered,
            cardPlay.Card,
            cardPlay);
    }
}