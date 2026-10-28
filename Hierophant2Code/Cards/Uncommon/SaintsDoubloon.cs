using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class SaintsDoubloon : Hierophant2Card
{
    public SaintsDoubloon() : base(1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.None)
    {
        WithTags(IsDoubloon);
        WithVar(new CardsVar(3).WithUpgrade(1));
        WithHoard(4, 0);
        WithKeyword(Tithe);
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
    }
}