using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Rebuild : Hierophant2Card
{
    public Rebuild() : base(3,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.None)
    {
        WithBlock(30, 6);
        WithKeyword(Tithe);
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (Creature creature in CombatState.Creatures)
        {
            await CreatureCmd.LoseBlock(choiceContext, creature, creature.Block, Owner.Creature);
        }
        foreach (Creature creature in CombatState.Creatures)
        {
            await CreatureCmd.GainBlock(creature, DynamicVars.Block, play, true);
        }
    }
}