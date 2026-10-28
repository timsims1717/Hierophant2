using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class ConvertCurrency : Hierophant2Card
{
    public ConvertCurrency() : base(0,
        CardType.Skill,
        CardRarity.Common,
        TargetType.Self)
    {
        WithGilded();
        WithEnergy(1, 1);
    }
    
    protected override bool ShouldGlowGoldInternal => PlayedDoubloonThisTurn(this);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (PlayedDoubloonThisTurn(play.Card))
        {
            await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
        }
    }
}