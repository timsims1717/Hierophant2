using Hierophant2.Hierophant2Code.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Mercenaries : Hierophant2Card
{
    public Mercenaries() : base(2,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithCalculatedDamage(0,
            (Func<CardModel, Creature, Decimal>)((card, _) => MercenaryCalc(card, _)));
        WithKeyword(Tithe);
        WithVar("InPlay", 0);
    }

    protected static decimal MercenaryCalc(CardModel card, Creature? creature)
    {
        decimal goldLost = GoldLostThisTurn(card, creature);
        if (card.DynamicVars["InPlay"].BaseValue == 0)
        {
            int diff = card.EnergyCost.GetAmountToSpend() - card.Owner.PlayerCombatState.Energy;
            int titheLost = diff * TitheAmount;
            if (titheLost > 0)
            {
                RelicModel? relic = card.Owner.GetRelic<HallowedStipend>();
                if (relic is HallowedStipend hs && hs.Count > 0)
                {
                    titheLost -= hs.Amount;
                }

                relic = card.Owner.GetRelic<ConsecratedStipend>();
                if (relic is ConsecratedStipend cs && cs.Count > 0)
                {
                    titheLost -= cs.Amount;
                }
            }

            if (titheLost > 0)
            {
                goldLost += titheLost;
            }
        }
        if (card.IsUpgraded)
        {
            return goldLost * 1.5M;
        }
        return goldLost;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        DynamicVars["InPlay"].BaseValue = 1;
        await DamageCmd.Attack(DynamicVars.CalculatedDamage)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
        DynamicVars["InPlay"].BaseValue = 0;
    }
}