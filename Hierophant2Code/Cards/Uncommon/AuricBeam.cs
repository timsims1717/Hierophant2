using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class AuricBeam : Hierophant2Card
{
    public AuricBeam() : base(2,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithVar("Percent", 10, -5);
        WithCalculatedDamage(0, 10,
            (card, _) => CurrentGoldPercent(card, 0.01M));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        decimal loseGold = DynamicVars.CalculatedDamage.Calculate(null);
        if (IsUpgraded)
        {
            loseGold /= 2;
        }
        await DamageCmd.Attack(DynamicVars.CalculatedDamage)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
        await PlayerCmd.LoseGold(loseGold, Owner);
        // await LoseGoldHook.TriggerGoldLoss(choiceContext, Owner, loseGold);
    }
}