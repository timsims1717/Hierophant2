using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class Zeal : Hierophant2Card
{
    public Zeal() : base(1,
        CardType.Attack,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithDamage(7);
        WithVar(new RepeatVar(2).WithUpgrade(1));
    }
    
    protected override bool ShouldGlowGoldInternal => ShouldThisCardGlow(this);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        int hitCount = play.Target.CurrentHp == play.Target.MaxHp ? DynamicVars.Repeat.IntValue : 1;
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .WithHitCount(hitCount)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
    }

    protected static bool ShouldThisCardGlow(CardModel card)
    {
        if (card.CombatState == null)
        {
            return false;
        }
        return card.CombatState.Enemies.Any(e => e.CurrentHp == e.MaxHp);
    }
}