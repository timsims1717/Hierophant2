using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class FieryStrike : Hierophant2Card
{
    public FieryStrike() : base(1,
        CardType.Attack,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithDamage(8, 2);
        WithFervor(5, 3);
        WithTags(CardTag.Strike);
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
        await PowerCmd.Apply<FervorPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<FervorPower>().BaseValue, 
            Owner.Creature, this);
    }
}