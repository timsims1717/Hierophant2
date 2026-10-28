using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class WrathOfGod : Hierophant2Card
{
    public WrathOfGod() : base(6,
        CardType.Attack,
        CardRarity.Rare,
        TargetType.AllEnemies)
    {
        WithCalculatedDamage(0, TotalCostOfCardsInDeck);
        WithKeyword(CardKeyword.Exhaust);
        WithKeyword(Tithe);
        UpgradeKeywords.Add((CardKeyword.Exhaust, UpgradeType.Remove));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.CalculatedDamage)
            .FromCard(this, play)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
    }
}