using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class ProsperityGospelPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.IsPlayer && cardPlay.Player == Owner.Player
            && (cardPlay.Card.Tags.Contains(Hierophant2Card.IsDoubloon) || cardPlay.Card.Keywords.Contains(Hierophant2Card.Gilded)))
        {
            Flash();
            await CreatureCmd.GainBlock(Owner,
                Amount,
                ValueProp.Unpowered,
                null);
        }
    }
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        await PowerCmd.Remove(this);
    }
}