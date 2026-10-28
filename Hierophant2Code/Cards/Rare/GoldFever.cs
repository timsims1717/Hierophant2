using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class GoldFever : Hierophant2Card
{
    public GoldFever() : base(2,
        CardType.Attack,
        CardRarity.Rare,
        TargetType.AnyEnemy)
    {
        WithDamage(12);
        WithPower<GoldFeverPower>(6, 5);
        WithGilded();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.IntValue)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
        await PowerCmd.Apply<GoldFeverPower>(choiceContext,
            play.Target,
            DynamicVars.Power<GoldFeverPower>().BaseValue, 
            Owner.Creature, 
            this);
    }
}