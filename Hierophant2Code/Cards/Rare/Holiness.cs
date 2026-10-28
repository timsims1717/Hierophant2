using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Holiness : Hierophant2Card
{
    public Holiness() : base(1,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithVar(new PowerVar<HolinessPower>(1).WithUpgrade(1));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<HolinessPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<HolinessPower>().IntValue,
            Owner.Creature, this);
    }
}