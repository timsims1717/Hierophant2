using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class Informant : Hierophant2Card
{
    public Informant() : base(0,
        CardType.Skill,
        CardRarity.Common,
        TargetType.Self)
    {
        WithGilded();
        WithBlock(7, 3);
    }
    
    protected override bool ShouldGlowGoldInternal => PlayedDoubloonThisTurn(this);
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (PlayedDoubloonThisTurn(play.Card))
        {
            await CommonActions.CardBlock(this, play);
        }
    }
}