using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Fellowship : Hierophant2Card
{
    public Fellowship() : base(-1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithVar("BlockAmount", 8);
    }
    
    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        int count = ResolveEnergyXValue();
        if (IsUpgraded)
        {
            count++;
        }
        await PowerCmd.Apply<FellowshipPower>(choiceContext,
            play.Player.Creature,
            count,
            play.Player.Creature,
            this);
        await PowerCmd.Apply<FellowshipPower>(choiceContext,
            play.Target,
            count,
            play.Player.Creature,
            this);
    }
}