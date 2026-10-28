using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Flagellation : Hierophant2Card
{
    public Flagellation() : base(1,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithDamage(5, 2);
        WithCalculatedVar("CalculatedHits", 1, GainedPietyThisTurnCount);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .WithHitCount((int) ((CalculatedVar) DynamicVars["CalculatedHits"]).Calculate(play.Target))
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
    }
}