using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Dazzle : Hierophant2Card
{
    public Dazzle() : base(2,
        CardType.Power,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithGilded();
        WithPower<DazzlePower>(1);
        UpgradeKeywords.Add((Tithe, UpgradeType.Add));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<DazzlePower>(choiceContext,
            Owner.Creature,
            DynamicVars[nameof(DazzlePower)].BaseValue,
            Owner.Creature,
            this);
    }
}