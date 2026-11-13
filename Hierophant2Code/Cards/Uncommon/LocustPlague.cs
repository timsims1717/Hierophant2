using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class LocustPlague : Hierophant2Card
{
    public LocustPlague() : base(1,
        CardType.Power,
        CardRarity.Uncommon,
        TargetType.Self)
    {
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (IsUpgraded)
        {
            await PowerCmd.Apply<LocustPlagueUpgradedPower>(choiceContext,
                play.Player.Creature,
                1,
                play.Player.Creature,
                this);
        }
        else
        {
            await PowerCmd.Apply<LocustPlaguePower>(choiceContext,
                play.Player.Creature,
                1,
                play.Player.Creature,
                this);
        }
    }
}