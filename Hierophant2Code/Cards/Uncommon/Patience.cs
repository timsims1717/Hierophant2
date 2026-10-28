using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Patience : Hierophant2Card
{
    public Patience() : base(1,
        CardType.Power,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithPower<PatiencePower>(2, 1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<PatiencePower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<PatiencePower>().BaseValue,
            Owner.Creature,
            this);
    }
}