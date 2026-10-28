using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Charity : Hierophant2Card
{
    public Charity() : base(1,
        CardType.Power,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithPower<CharityPower>(3, 1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<CharityPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<CharityPower>().BaseValue,
            Owner.Creature,
            this);
    }
}