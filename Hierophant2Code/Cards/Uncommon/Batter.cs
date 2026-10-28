using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Batter : Hierophant2Card
{
    public Batter() : base(1,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AllEnemies)
    {
        WithDamage(7, 3);
    }
    
    protected override bool ShouldGlowGoldInternal => AnyEnemyHasBlock();

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<Creature> cList = CombatState.Enemies
            .Where(c => c is { IsEnemy: true, Block: > 0 }).ToList();
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
        foreach (Creature c in cList)
        {
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this, play)
                .Targeting(c)
                .WithHitFx("vfx/vfx_attack_blunt")
                .Execute(choiceContext);
        }
    }
}