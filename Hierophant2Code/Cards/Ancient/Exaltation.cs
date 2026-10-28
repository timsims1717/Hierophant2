using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Ancient;

public class Exaltation : Hierophant2Card
{
    public Exaltation() : base(2,
        CardType.Power,
        CardRarity.Ancient,
        TargetType.Self)
    {
        WithPower<ExaltedPower>(2, 1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ExaltedPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<ExaltedPower>().IntValue, 
            Owner.Creature,
            this);
    }
}