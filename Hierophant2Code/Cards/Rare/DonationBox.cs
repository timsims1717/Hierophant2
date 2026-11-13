using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class DonationBox : Hierophant2Card
{
    public DonationBox() : base(2,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<DonationBoxPower>(1, 1);
        WithHoardTip();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<DonationBoxPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<DonationBoxPower>().IntValue,
            Owner.Creature,
            this);
    }
}