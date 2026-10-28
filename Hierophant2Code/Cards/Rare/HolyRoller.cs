using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class HolyRoller : Hierophant2Card
{
    public HolyRoller() : base(-1,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithTags(IsHoly);
        WithTitheTip();
    }
    
    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int count = ResolveEnergyXValue();
        if (IsUpgraded)
        {
            count++;
        }

        foreach (CardModel card in CardFactory.GetForCombat(Owner, Owner.Character.CardPool.GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint), 
            count, Owner.RunState.Rng.CombatCardGeneration))
        {
            if (CanHaveTithe(card))
            {
                card.AddKeyword(Tithe);
            }
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner);
        }
    }
}