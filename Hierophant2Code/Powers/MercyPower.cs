using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class MercyPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterBlockGained(
        Creature creature,
        decimal amount,
        ValueProp props,
        CardModel? cardSource)
    {
        if (!creature.IsEnemy || cardSource == null || !Owner.IsPlayer || cardSource.Owner != Owner.Player)
        {
            return;
        }
        await PowerCmd.Apply<VulnerablePower>(new ThrowingPlayerChoiceContext(),
            creature,
            Amount,
            Owner, null);
        await PowerCmd.Apply<WeakPower>(new ThrowingPlayerChoiceContext(),
            creature,
            Amount,
            Owner, null);
    }
}