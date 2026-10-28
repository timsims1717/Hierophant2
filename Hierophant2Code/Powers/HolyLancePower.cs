using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Powers;

public class HolyLancePower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override bool TryModifyEnergyCostInCombatLate(
        CardModel card,
        Decimal originalCost,
        out Decimal modifiedCost)
    {
        modifiedCost = originalCost;
        if (card.Owner.Creature != Owner || !card.Tags.Contains(Hierophant2Card.IsHoly))
        {
            return false;
        }

        PileType? type = card.Pile?.Type;
        if (type.HasValue)
        {
            switch (type.GetValueOrDefault())
            {
                case PileType.Hand:
                case PileType.Play:
                    modifiedCost = 0M;
                    return true;
            }
        }
        return false;
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != Owner || !cardPlay.Card.Tags.Contains(Hierophant2Card.IsHoly))
        {
            return;
        }

        PileType? type = cardPlay.Card.Pile?.Type;
        if (type.HasValue)
        {
            switch (type.GetValueOrDefault())
            {
                case PileType.Hand:
                case PileType.Play:
                    Flash();
                    await PowerCmd.Decrement(this);
                    return;
            }
        }
    }
}