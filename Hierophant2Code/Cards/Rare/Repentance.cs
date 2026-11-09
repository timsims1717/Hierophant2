using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Repentance : Hierophant2Card
{
    public Repentance() : base(1,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithCalculatedVar("PietyGained", 0, CurrentPiety);
        WithPietyTip();
        WithKeyword(CardKeyword.Exhaust);
        UpgradeKeywords.Add((CardKeyword.Exhaust, UpgradeType.Remove));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<PietyPower>(choiceContext,
            play.Player.Creature,
            play.Card.Owner.Creature.GetPowerAmount<PietyPower>(),
            play.Player.Creature,
            this);
    }
}