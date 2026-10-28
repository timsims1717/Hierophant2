using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class TollTheBells : Hierophant2Card
{
    public TollTheBells() : base(1,
        CardType.Attack,
        CardRarity.Common,
        TargetType.AllEnemies)
    {
        WithDamage(7, 3);
        WithBlock(5, 2);
    }
    
    protected override bool ShouldGlowGoldInternal => GainedPietyThisTurnCount(this, null) > 0;
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).TargetingAllOpponents(CombatState).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);
        if (GainedPietyThisTurnCount(cardPlay.Card, null) > 0)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        }
    }
}