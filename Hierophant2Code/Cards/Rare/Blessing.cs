using BaseLib.Extensions;
using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Blessing : Hierophant2Card
{
    public Blessing() : base(1,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.AnyEnemy)
    {
        WithVar("EnemyStrength", 1);
        WithEnemyBlock(5, 0);
        WithPower<StrengthPower>(1, 1);
        WithBlock(5, 2);
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await PowerCmd.Apply<StrengthPower>(choiceContext,
            play.Target,
            DynamicVars["EnemyStrength"].IntValue, 
            Owner.Creature, 
            this);
        await CreatureCmd.GainBlock(play.Target,
            DynamicVars[EnemyBlockVar.DefaultName].IntValue,
            ValueProp.Move,
            play);
        await PowerCmd.Apply<StrengthPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<StrengthPower>().IntValue, 
            Owner.Creature, 
            this);
        await CommonActions.CardBlock(this, play);
    }
}