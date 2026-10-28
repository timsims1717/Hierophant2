using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Remorse : Hierophant2Card
{
    public Remorse() : base(1,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithCalculatedDamage(6, 3,
            (Func<CardModel, Creature, Decimal>)((card, _) => DoubloonsInExhaustPile(card, _)),
            ValueProp.Move, 0, 1);
        WithGilded();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.CalculatedDamage)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
    }
}