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
        WithKeyword(CardKeyword.Exhaust);
        UpgradeKeywords.Add((CardKeyword.Exhaust, UpgradeType.Remove));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<RepentancePower>(choiceContext,
            play.Player.Creature,
            1,
            play.Player.Creature,
            this);
    }
}