using Hierophant2.Hierophant2Code.Cards.Token;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Powers;

public class CursedGoldPower() : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        if (Owner.IsPlayer)
        {
            IEnumerable<Doubloon> doubloons = Owner.Player.PlayerCombatState.AllCards.OfType<Doubloon>();
            foreach (Doubloon doubloon in doubloons)
            {
                doubloon.RemoveKeyword(CardKeyword.Exhaust);
            }
        }
    }

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (Owner.IsPlayer && Owner.Player == card.Owner && card is Doubloon)
        {
            card.RemoveKeyword(CardKeyword.Exhaust);
        }
    }
}