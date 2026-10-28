using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class AuricShield : Hierophant2Card
{
    public AuricShield() : base(1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithKeyword(CardKeyword.Retain);
        WithVar("Percent", 5, 3);
        WithCalculatedBlock(0, 5,
            (card, _) => CurrentGoldPercent(card, 0.01M),
            ValueProp.Move, 0, 3);
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        decimal loseGold = DynamicVars.CalculatedBlock.Calculate(null);
        await CreatureCmd.GainBlock(Owner.Creature,
            DynamicVars.CalculatedBlock.Calculate(play.Target),
            DynamicVars.CalculatedBlock.Props,
            play);
        await PlayerCmd.LoseGold(loseGold, Owner);
    }
}