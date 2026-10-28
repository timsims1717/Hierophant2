using Hierophant2.Hierophant2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class VervePower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override object InitInternalData() => new Data();
    
    public override Task BeforeAttack(AttackCommand command)
    {
        if (command.Attacker != Owner
            || !command.DamageProps.IsPoweredAttack()
            || !Owner.HasPower<FervorPower>()
            || command.ModelSource == null
            || !(command.ModelSource is CardModel)
            || ((CardModel)command.ModelSource).Tags.Contains(Hierophant2Card.KeepFervor))
        {
            return Task.CompletedTask;
        }

        Data internalData = GetInternalData<Data>();
        internalData.CommandsToModify.Add(command);
        return Task.CompletedTask;
    }
    
    public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
    {
        Data internalData = GetInternalData<Data>();
        if (!internalData.CommandsToModify.Contains(command))
        {
            return;
        }

        Flash();
        await CardPileCmd.Draw(choiceContext, Amount, Owner.Player);
        internalData.CommandsToModify.Remove(command);
    }
    
    // private int _fervorAmount;

    // protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip>(HoverTipFactory.FromPower<FervorPower>());

    // public async override Task BeforeCardPlayed(CardPlay cardPlay)
    // {
    //     if (Owner.IsPlayer && cardPlay.Player == Owner.Player && Owner.HasPower<FervorPower>())
    //     {
    //         _fervorAmount = Owner.GetPowerAmount<FervorPower>();
    //     }
    //     else
    //     {
    //         _fervorAmount = 0;
    //     }
    // }
    //
    // public async override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    // {
    //     if (Owner.IsPlayer && cardPlay.Player == Owner.Player
    //        && cardPlay.Card.Type == CardType.Attack
    //        && _fervorAmount > 0 && (!Owner.HasPower<FervorPower>() || Owner.GetPowerAmount<FervorPower>() < _fervorAmount)
    //        && !cardPlay.Card.Tags.Contains(Hierophant2Card.KeepFervor))
    //     {
    //         Flash();
    //         await CardPileCmd.Draw(choiceContext, Amount, Owner.Player);
    //     }
    //
    //     _fervorAmount = 0;
    // }
    
    // public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext,
    //     Creature? dealer, DamageResult result, ValueProp props,
    //     Creature target, CardModel? cardSource)
    // {
    //     if (Owner.IsPlayer && dealer != null && dealer == Owner
    //         && cardSource != null && cardSource.Type == CardType.Attack
    //         && !cardSource.Tags.Contains(Hierophant2Card.KeepFervor))
    //     {
    //         Flash();
    //         await CardPileCmd.Draw(choiceContext, Amount, Owner.Player);
    //     }
    // }
    
    private class Data
    {
        public Data()
        {
            CommandsToModify = new List<AttackCommand>();
        }
        public List<AttackCommand?> CommandsToModify;
    }
}