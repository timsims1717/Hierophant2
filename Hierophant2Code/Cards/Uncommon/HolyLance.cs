using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class HolyLance : Hierophant2Card
{
    public HolyLance() : base(1,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithDamage(9, 3);
        WithTags(IsHoly);
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
        await PowerCmd.Apply<HolyLancePower>(choiceContext, 
            Owner.Creature,
            1, 
            Owner.Creature, this);
    }
}