using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class OrnateBuckler : Hierophant2Card
{
    
    public OrnateBuckler() : base(1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.Self)
    {
        WithBlock(8, 2);
        WithHoard(3, 1);
    }
    
    public override bool CanBeGeneratedInCombat => false;
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
    }
}