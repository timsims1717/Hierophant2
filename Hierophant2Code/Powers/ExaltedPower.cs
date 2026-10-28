using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Powers;

public class ExaltedPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        new List<DynamicVar> { new ("Increase", Amount), new ("CurrentAmount", Amount) };

    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    
    public override int DisplayAmount => DynamicVars["CurrentAmount"].IntValue;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        DynamicVars["Increase"].BaseValue = Amount;
        DynamicVars["CurrentAmount"].BaseValue = Amount;
        InvokeDisplayAmountChanged();
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (Owner.IsPlayer && cardPlay.Player == Owner.Player
            && cardPlay.Card.Type == CardType.Attack)
        {
            Flash();
            await PowerCmd.Apply<FervorPower>(choiceContext,
                Owner,
                DynamicVars["CurrentAmount"].IntValue, 
                Owner, 
                null);
            DynamicVars["CurrentAmount"].UpgradeValueBy(DynamicVars["Increase"].IntValue);
            InvokeDisplayAmountChanged();
        }
    }
}