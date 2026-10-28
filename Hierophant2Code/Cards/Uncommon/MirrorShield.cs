using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class MirrorShield : Hierophant2Card
{
    public MirrorShield() : base(0,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithCalculatedBlock(0,
            (_, creature) => GetEnemyBlock(_, creature));
        WithCards(1);
    }
    
    protected override bool ShouldGlowGoldInternal => AnyEnemyHasBlock();
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await CreatureCmd.GainBlock(play.Card.Owner.Creature,
            DynamicVars.CalculatedBlock.Calculate(play.Target),
            DynamicVars.CalculatedBlock.Props, play);
        if (IsUpgraded)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }
    }
}