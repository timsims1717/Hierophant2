using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class DonationBox : Hierophant2Card
{
    public DonationBox() : base(0,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithCalculatedVar("CardsToDraw", 0, TotalCostOfCardsInHand);
        WithCards(4, 1);
        WithHoard(6, 2);
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext,
            ((CalculatedVar)DynamicVars["CardsToDraw"]).Calculate(null), Owner);
        if (cardModels.Count() >= DynamicVars.Cards.BaseValue)
        {
            await CardCmd.Exhaust(choiceContext, this);
        }
    }
}