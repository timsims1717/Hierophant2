using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Generosity : Hierophant2Card
{
    public Generosity() : base(1,
        CardType.Power,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithPower<GenerosityPower>(1);
        WithGilded();
        UpgradeKeywords.Add((CardKeyword.Innate, UpgradeType.Add));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<GenerosityPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<GenerosityPower>().IntValue,
            Owner.Creature,
            this);
    }
}