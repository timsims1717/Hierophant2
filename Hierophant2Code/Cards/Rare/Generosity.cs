using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Generosity : Hierophant2Card
{
    public Generosity() : base(2,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<GenerosityPower>(2, 1);
        WithGilded();
        UpgradeKeywords.Add((Tithe, UpgradeType.Add));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<GenerosityPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<GenerosityPower>().BaseValue,
            Owner.Creature,
            this);
    }
}