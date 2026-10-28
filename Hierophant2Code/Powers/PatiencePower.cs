using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class PatiencePower() : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public bool OffThisTurn;

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext,
        Creature? dealer, DamageResult result, ValueProp props,
        Creature target, CardModel? cardSource)
    {
        if (dealer == null || dealer != Owner || dealer == target
            || cardSource == null || cardSource.Type != CardType.Attack)
        {
            return;
        }

        if (result.WasFullyBlocked || result.UnblockedDamage == 0)
        {
            return;
        }
        OffThisTurn = true;
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (!OffThisTurn)
        {
            await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, 
                Owner,
                Amount, 
                Owner,
                null);
        }
        OffThisTurn = false;
    }
}