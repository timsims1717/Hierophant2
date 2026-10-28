using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Testify : Hierophant2Card
{
    public Testify() : base(0,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<TestifyPower>(2, 1);
        WithPietyTip();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<TestifyPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<TestifyPower>().IntValue, 
            Owner.Creature, this);
    }
}