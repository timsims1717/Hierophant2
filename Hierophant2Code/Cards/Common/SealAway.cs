using Hierophant2.Hierophant2Code.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class SealAway : Hierophant2Card
{
    public SealAway() : base(1,
        CardType.Attack,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithDamage(10);
        WithEnemyBlock(10, 0);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
        await CreatureCmd.GainBlock(play.Target, DynamicVars[EnemyBlockVar.DefaultName].IntValue, ValueProp.Move, play);
    }
}