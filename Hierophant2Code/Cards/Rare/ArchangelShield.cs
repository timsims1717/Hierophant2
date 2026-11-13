using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class ArchangelShield : Hierophant2Card
{
    public ArchangelShield() : base(2,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<ArchangelShieldPower>(1);
        WithCostUpgradeBy(-1);
        WithTip(StaticHoverTip.Block);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ArchangelShieldPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<ArchangelShieldPower>().IntValue,
            Owner.Creature,
            this);
    }
}