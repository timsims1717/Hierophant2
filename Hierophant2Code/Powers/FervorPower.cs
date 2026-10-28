using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class FervorPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    protected override object InitInternalData() => new Data();
    
    public override Task BeforeAttack(AttackCommand command)
    {
        if (command.Attacker != Owner || !command.DamageProps.IsPoweredAttack())
        {
            return Task.CompletedTask;
        }

        Data internalData = GetInternalData<Data>();
        if (internalData.commandToModify != null
            || command.ModelSource != null
            && !(command.ModelSource is CardModel) || !command.DamageProps.IsPoweredAttack()
            || ((CardModel)command.ModelSource).Tags.Contains(Hierophant2Card.KeepFervor))
        {
            return Task.CompletedTask;
        }

        internalData.commandToModify = command;
        internalData.amountWhenAttackStarted = Amount;
        return Task.CompletedTask;
    }

    public override decimal ModifyDamageMultiplicative(Creature? target,
        decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource, CardPlay? cardPlay)
    {
        if (Owner != dealer || !props.IsPoweredAttack())
        {
            return 1M;
        }
        
        Data internalData = GetInternalData<Data>();
        return internalData.commandToModify != null
            && cardSource != null
            && cardSource != internalData.commandToModify.ModelSource
            || internalData.commandToModify != null
            && internalData.commandToModify.Attacker != dealer ? 1M : 1 + Amount * (decimal)0.1;
        
        // if (cardSource != null && cardSource.Owner.Creature == dealer) {
        //     return 1 + Amount * (decimal)0.1;
        // }
        //
        // return 1;
    }

    public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
    {
        Data internalData = GetInternalData<Data>();
        if (command != internalData.commandToModify)
        {
            return;
        }

        Flash();
        await PowerCmd.ModifyAmount(choiceContext, this, -internalData.amountWhenAttackStarted, null, null);
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (Owner.Side == side && !Owner.HasPower<EnlightenedPower>())
        {
            PowerCmd.Remove(this);
        }
    }
    
    private class Data
    {
        public AttackCommand? commandToModify;
        public int amountWhenAttackStarted;
    }
}