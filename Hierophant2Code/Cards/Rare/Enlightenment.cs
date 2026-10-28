using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Enlightenment : Hierophant2Card
{
    public Enlightenment() : base(0,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        UpgradeKeywords.Add((CardKeyword.Retain, UpgradeType.Add));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<EnlightenedPower>(choiceContext,
            Owner.Creature,
            1,
            Owner.Creature,
            this);
    }
}