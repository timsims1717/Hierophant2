using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Fellowship : Hierophant2Card
{
    public Fellowship() : base(1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithPower<FellowshipPower>(1);
        WithKeyword(CardKeyword.Exhaust);
        UpgradeKeywords.Add((CardKeyword.Innate, UpgradeType.Add));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await PowerCmd.Apply<FellowshipPower>(choiceContext,
            play.Target,
            DynamicVars.Power<FellowshipPower>().IntValue,
            play.Player.Creature,
            this);
    }
}