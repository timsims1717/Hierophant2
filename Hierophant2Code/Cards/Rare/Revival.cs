using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Revival : Hierophant2Card
{
    public Revival() : base(0,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPietyTip();
        WithFervorTip();
        WithKeyword(CardKeyword.Exhaust);
        UpgradeKeywords.Add((CardKeyword.Exhaust, UpgradeType.Remove));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int piety = Owner.Creature.GetPowerAmount<PietyPower>();
        int fervor = Owner.Creature.GetPowerAmount<FervorPower>();
        await PowerCmd.Apply<PietyPower>(choiceContext,
            Owner.Creature,
            fervor,
            Owner.Creature,
            this);
        await PowerCmd.Apply<FervorPower>(choiceContext,
            Owner.Creature,
            piety,
            Owner.Creature,
            this);
    }
}