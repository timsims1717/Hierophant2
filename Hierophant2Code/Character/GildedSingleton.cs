using BaseLib.Abstracts;
using Hierophant2.Hierophant2Code.Cards;
using Hierophant2.Hierophant2Code.Cards.Token;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Character;

public class GildedSingleton() : CustomSingletonModel(HookType.Run)
{
    public override async Task AfterCardChangedPilesLate(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
    {
        if (card.Owner.Creature.IsDead)
        {
            return;
        }
        CardPile pile = card.Pile;
        if (pile == null || pile.Type != PileType.Deck)
        {
            return;
        }

        if (card.CanonicalKeywords.Contains(Hierophant2Card.Gilded))
        {
            CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(card.Owner.RunState.CreateCard<Doubloon>(card.Owner), PileType.Deck), 1.5f);
        }
    }
}