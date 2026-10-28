using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class LayOnHands : Hierophant2Card
{
    public LayOnHands() : base(0,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithDamage(6, 2);
        WithPower<FervorPower>(6, 2);
        WithPower<PietyPower>(6, 2);
    }
    
    protected override bool ShouldGlowGoldInternal => AnyEnemyHasBlock();

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        bool hadBlock = GetEnemyBlock(play.Card, play.Target) > 0;
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
        if (hadBlock)
        {
            await PowerCmd.Apply<FervorPower>(choiceContext,
                Owner.Creature,
                DynamicVars.Power<FervorPower>().BaseValue,
                Owner.Creature, this);
            await PowerCmd.Apply<PietyPower>(choiceContext,
                Owner.Creature,
                DynamicVars.Power<PietyPower>().BaseValue,
                Owner.Creature, this);
        }
    }
}