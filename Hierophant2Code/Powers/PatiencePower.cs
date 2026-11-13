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

    public override async Task AfterCardPlayed(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (!Owner.IsPlayer || cardPlay.Player != Owner.Player 
            || cardPlay.Card.Type != CardType.Attack)
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