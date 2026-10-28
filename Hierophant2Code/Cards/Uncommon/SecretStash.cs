using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class SecretStash : Hierophant2Card
{
    public SecretStash() : base(1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.None)
    {
        WithHoard(20, 5);
        WithKeyword(CardKeyword.Ethereal);
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
    }
}