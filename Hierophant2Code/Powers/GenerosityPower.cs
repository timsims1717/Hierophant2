using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Powers;

public class GenerosityPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card.Owner.Creature != Owner || !card.Tags.Contains(Hierophant2Card.IsDoubloon)
            || CombatManager.Instance.History.Entries
                .OfType<CardDrawnEntry>().Count(e =>
                    e.HappenedThisTurn(CombatState) && e.Actor == Owner && card.Tags.Contains(Hierophant2Card.IsDoubloon)) > 1)
        {
            return;
        }

        Flash();
        await CardPileCmd.Draw(choiceContext, Amount, Owner.Player);
    }
}