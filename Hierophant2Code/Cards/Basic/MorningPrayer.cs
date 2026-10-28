using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Basic;

public class MorningPrayer : Hierophant2Card
{
    public MorningPrayer() : base(2,
        CardType.Skill,
        CardRarity.Basic,
        TargetType.Self)
    {
        WithBlock(8, 3);
        WithKeyword(Tithe);
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
    }
}