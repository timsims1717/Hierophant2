using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Upheaval : Hierophant2Card
{
    public Upheaval() : base(2,
        CardType.Attack,
        CardRarity.Rare,
        TargetType.AnyEnemy)
    {
        WithCalculatedDamage(0, GetEnemyBlock);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await CreatureCmd.LoseBlock(choiceContext, play.Target, GetEnemyBlock(play.Card, play.Target), play.Player.Creature);
        await DamageCmd.Attack(DynamicVars.CalculatedDamage)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
    }
}