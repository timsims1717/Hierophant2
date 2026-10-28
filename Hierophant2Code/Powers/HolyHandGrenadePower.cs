using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Powers;

public class HolyHandGrenadePower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        new List<DynamicVar>([new DamageVar(30M, ValueProp.Unpowered)]);

    public void SetDamage(Decimal damage)
    {
        DynamicVars.Damage.BaseValue = damage;
    }
    
    public override async Task AfterSideTurnStart(CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (side == Owner.Side)
        {
            Flash();
            if (Amount <= 1)
            {
                if (NCombatRoom.Instance != null)
                {
                    NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(
                        (Node)NFireSmokePuffVfx.Create(Owner));
                }

                await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(),
                    Owner,
                    DynamicVars.Damage.IntValue, 
                    ValueProp.Unpowered, 
                    null,
                    null);
                await PowerCmd.Remove(this);
            }
            else
            {
                await PowerCmd.Decrement(this);
            }
        }
    }
}