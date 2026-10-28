using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Basic;

public class DefendHierophant : Hierophant2Card
{
    public DefendHierophant() : base(1,
        CardType.Skill,
        CardRarity.Basic,
        TargetType.Self)
    {
        WithBlock(5, 3);
        WithTags(CardTag.Defend);
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }
}