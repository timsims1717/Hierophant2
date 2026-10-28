using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class AuricForm : Hierophant2Card
{
    public AuricForm() : base(3,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<AuricFormPower>(5, 3);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<AuricFormPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<AuricFormPower>().IntValue,
            Owner.Creature,
            this);
    }
}