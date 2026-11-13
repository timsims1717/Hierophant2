using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class AuricForm : Hierophant2Card
{
    public AuricForm() : base(3,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithVar("Percent", 1, 1);
        WithTip(typeof(PlatingPower));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        decimal loseGold = play.Player.Gold * DynamicVars["Percent"].IntValue * 0.01M;
        await PowerCmd.Apply<PlatingPower>(choiceContext,
            Owner.Creature,
            loseGold,
            Owner.Creature,
            this);
    }
}