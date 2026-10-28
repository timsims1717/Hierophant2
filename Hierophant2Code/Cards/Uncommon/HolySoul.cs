using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class HolySoul : Hierophant2Card
{
    public HolySoul() : base(1,
        CardType.Power,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithVar(new PowerVar<HolySoulPower>(5).WithUpgrade(2));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<HolySoulPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<HolySoulPower>().IntValue,
            Owner.Creature, this);
    }
}