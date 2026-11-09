using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class RustyCrate : Hierophant2Card
{
    public RustyCrate() : base(1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.None)
    {
        WithCards(1, 1);
        WithHoard(2,0);
        WithTitheTip();
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IEnumerable<CardModel> drawn = await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        foreach (CardModel card in drawn)
        {
            ApplySingleTurnTithe(card);
        }
    }
}