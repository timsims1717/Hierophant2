using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Mercy : Hierophant2Card
{
    public Mercy() : base(1,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<MercyPower>(1);
        WithCostUpgradeBy(-1);
        WithTip(typeof(VulnerablePower));
        WithTip(typeof(WeakPower));
        WithTip(StaticHoverTip.Block);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<MercyPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<MercyPower>().IntValue,
            Owner.Creature,
            this);
    }
}