using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Verve : Hierophant2Card
{
    public Verve() : base(1,
        CardType.Power,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithPower<VervePower>(1, 1);
        WithFervorTip();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    { 
        await PowerCmd.Apply<VervePower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<VervePower>().BaseValue,
            Owner.Creature,
            this);
    }
}