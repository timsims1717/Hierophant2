using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class RustyCrate : Hierophant2Card
{
    public RustyCrate() : base(-1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.None)
    {
        WithKeyword(CardKeyword.Unplayable);
        WithHoard(5, 5);
        WithGilded(2);
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {}
}