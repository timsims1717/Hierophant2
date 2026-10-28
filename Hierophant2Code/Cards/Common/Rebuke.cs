using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class Rebuke : Hierophant2Card
{
    protected bool PlayedThisTurn;
    
    public Rebuke() : base(0,
        CardType.Attack,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithDamage(4, 2);
    }
    
    protected override bool ShouldGlowGoldInternal => ShouldThisRebukeGlow(this);

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
        if (!PlayedThisTurn)
        {
            foreach (CardModel card in (IEnumerable<CardModel>)PileType.Discard.GetPile(Owner).Cards
                        .Where(Filter).ToList())
            {
                await CardPileCmd.Add(card, PileType.Hand);
            }
            PlayedThisTurn = true;
        }
    }
    
    private bool Filter(CardModel card)
    {
        return card.Id == Id;
    }

    public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        PlayedThisTurn = false;
        return base.BeforeSideTurnStart(choiceContext, side, participants, combatState);
    }

    public static bool ShouldThisRebukeGlow(Rebuke card)
    {
        return !card.PlayedThisTurn;
    }
}