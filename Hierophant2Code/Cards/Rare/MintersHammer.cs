using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Cards.Token;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class MintersHammer : Hierophant2Card
{
    public MintersHammer() : base(1,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithVar(new PowerVar<FervorPower>(8).WithUpgrade(4));
        WithGilded();
        WithKeyword(CardKeyword.Exhaust);
    }
    
    public override bool CanBeGeneratedInCombat => false;
    protected override bool ShouldGlowGoldInternal => ShouldThisCardGlow(this);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CardSelectorPrefs prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1)
        {
            PretendCardsCanBePlayed = true
        };
        CardModel? card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, IsAnUnupgradedDoubloon, this)).FirstOrDefault();
        if (card != null)
        {
            CardCmd.Upgrade(card);
            if (!(DeckVersion is Doubloon deckVersion))
                return;
            CardCmd.Upgrade(deckVersion);
        }
        await PowerCmd.Apply<FervorPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<FervorPower>().BaseValue, 
            Owner.Creature, this);
    }
    
    protected static bool ShouldThisCardGlow(CardModel card)
    {
        CardPile? hand = CardPile.Get(PileType.Hand, card.Owner);
        if (hand == null)
        {
            return false;
        }
        return hand.Cards.Any(c => c is Doubloon && !c.IsUpgraded);
    }
}