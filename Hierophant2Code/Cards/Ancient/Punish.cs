using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Ancient;

public class Punish : Hierophant2Card
{
    public Punish() : base(1,
        CardType.Attack,
        CardRarity.Ancient,
        TargetType.AllEnemies)
    {
        WithDamage(18, 4);
        WithPiety(18, 4);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
        await PowerCmd.Apply<PietyPower>(choiceContext, Owner.Creature, DynamicVars[nameof(PietyPower)].BaseValue, Owner.Creature, this);
    }
}