using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class DoomSaying : Hierophant2Card
{
    public DoomSaying() : base(1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithCards(3, 1);
        WithEnergy(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int energy = 0; 
        if (!CombatManager.Instance.IsOverOrEnding)
        {
            CardPile drawPile = PileType.Draw.GetPile(Owner);
            int count = Math.Max(0, Math.Min(DynamicVars.Cards.IntValue, drawPile.Cards.Count));
            int i = 0;
            foreach (CardModel card in drawPile.Cards)
            {
                if (!card.Owner.Creature.IsDead)
                {
                    energy += card.EnergyCost.Canonical;
                    i++;
                    if (i == count)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        if (energy > 0)
        {
            await PlayerCmd.GainEnergy(energy * DynamicVars.Energy.IntValue, Owner);
        }
    }
}