using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Powers;

public class EndurePower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext,
        CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player && Owner.IsPlayer)
        {
            Flash();
            CardSelectorPrefs prefs = new CardSelectorPrefs(SelectionScreenPrompt, Amount);
            CardModel card = (await CardSelectCmd.FromCombatPile(choiceContext,
                    PileType.Discard.GetPile(Owner.Player),
                    Owner.Player,
                    prefs)).FirstOrDefault();
            if (card == null)
            {
                return;
            }

            CardCmd.ApplySingleTurnRetain(card);
            await CardPileCmd.Add(card, PileType.Hand);
            await PowerCmd.Remove(this);
        }
    }
}