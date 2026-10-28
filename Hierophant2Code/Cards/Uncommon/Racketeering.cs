using Hierophant2.Hierophant2Code.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Racketeering : Hierophant2Card
{
    public Racketeering() : base(0,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithEnemyBlock(10, 5);
        WithVar(new DynamicVar("BlockPerCard", 5));
        WithCards(1);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await CreatureCmd.GainBlock(play.Target,
            DynamicVars[EnemyBlockVar.DefaultName].IntValue, 
            ValueProp.Move, play);
        int blockOnEnemy = play.Target.Block;
        await CardPileCmd.Draw(choiceContext,
            blockOnEnemy / (DynamicVars.Cards.IntValue * DynamicVars["BlockPerCard"].IntValue),
            play.Player);
    }
}