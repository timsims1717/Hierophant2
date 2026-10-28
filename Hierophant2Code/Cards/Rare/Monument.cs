using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Monument : Hierophant2Card
{
    public Monument() : base(2,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithCalculatedBlock(5, 2, GoldLostThisCombat);
        WithHoard(15, 5);
        WithVar(new GoldVar(5));
    }
    
    public override bool CanBeGeneratedInCombat => false;
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, 
            DynamicVars.CalculatedBlock.Calculate(play.Target),
            DynamicVars.CalculatedBlock.Props, play);
        await PlayerCmd.LoseGold(DynamicVars.Gold.IntValue, Owner);
    }
}