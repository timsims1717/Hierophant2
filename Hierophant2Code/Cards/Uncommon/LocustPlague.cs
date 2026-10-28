using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class LocustPlague : Hierophant2Card
{
    public LocustPlague() : base(3,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AllEnemies)
    {
        WithDamage(4, 1);
        WithCalculatedVar("CalculatedHits", 0, TotalCostOfCardsInHand);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .WithHitCount((int) ((CalculatedVar) DynamicVars["CalculatedHits"]).Calculate(null))
            .FromCard(this, play)
            .TargetingRandomOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }
}