using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class Encroach : Hierophant2Card
{
    public Encroach() : base(1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.Self)
    {
        WithBlock(7, 2);
        WithCards(2);
    }
    
    protected override bool ShouldGlowGoldInternal => AnyEnemyHasBlock();
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        if (AnyEnemyHasBlock())
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }
    }
}