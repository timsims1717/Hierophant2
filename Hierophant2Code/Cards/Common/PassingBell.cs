using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class PassingBell : Hierophant2Card
{
    public PassingBell() : base(1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithPiety(4, 3);
        WithPower<VulnerablePower>(1, 1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        await PowerCmd.Apply<PietyPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<PietyPower>().IntValue, 
            Owner.Creature, 
            this);
        await PowerCmd.Apply<VulnerablePower>(choiceContext,
            cardPlay.Target,
            DynamicVars.Power<VulnerablePower>().IntValue,
            Owner.Creature,
            this);
    }
}