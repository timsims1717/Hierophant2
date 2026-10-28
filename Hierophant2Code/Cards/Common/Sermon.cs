using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class Sermon : Hierophant2Card
{
    public Sermon() : base(1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.AllEnemies)
    {
        WithPower<WeakPower>(1, 1);
        WithFervor(5, 0);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState == null)
        {
            return;
        }
        foreach (Creature enemy in CombatState.Enemies)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, enemy,
                DynamicVars.Power<WeakPower>().BaseValue,
                Owner.Creature, this);
        }
        await PowerCmd.Apply<FervorPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<FervorPower>().BaseValue,
            Owner.Creature, this);
    }
}