using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class HolyWar : Hierophant2Card
{
    public HolyWar() : base(2,
        CardType.Attack,
        CardRarity.Rare,
        TargetType.AnyEnemy)
    {
        WithCalculatedDamage(16, 4,
            HolyWarMult, ValueProp.Move,
            2, 2);
        WithTags(IsHoly);
    }

    private int _holyCount;

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

    public static decimal HolyWarMult(CardModel card, Creature? _)
    {
        return card is HolyWar war ? war._holyCount : 0;
    }

    public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner == Owner
            && cardPlay.Card.Tags.Contains(IsHoly))
        {
            _holyCount++;
        }
    }
}