using Hierophant2.Hierophant2Code.Character;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class HammerAndAnvil : Hierophant2Card
{
    public HammerAndAnvil() : base(1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithEnemyBlock(8, 4);
        WithFervor(8, 4);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await CreatureCmd.GainBlock(play.Target, DynamicVars[EnemyBlockVar.DefaultName].IntValue, ValueProp.Move, play);
        await PowerCmd.Apply<FervorPower>(choiceContext, 
            Owner.Creature,
            DynamicVars[nameof(FervorPower)].BaseValue, 
            Owner.Creature, this);
    }
}