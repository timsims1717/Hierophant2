using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class TollTheBells : Hierophant2Card
{
    public TollTheBells() : base(1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithPiety(8, 3);
        WithCalculatedVar("CalculatedPiety", 0, NumberOfEnemies);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int c = (int) ((CalculatedVar)DynamicVars["CalculatedPiety"]).Calculate(null);
        for (int i = 0; i < c; i++)
        {
            await PowerCmd.Apply<PietyPower>(choiceContext,
                play.Player.Creature,
                DynamicVars.Power<PietyPower>().IntValue,
                play.Player.Creature,
                this);
        }
    }

    public static decimal NumberOfEnemies(CardModel card, Creature? _)
    {
        if (card.Owner.Creature.CombatState != null)
        {
            return card.Owner.Creature.CombatState.Enemies.Count(c => c.IsAlive);
        }

        return 0;
    }
}