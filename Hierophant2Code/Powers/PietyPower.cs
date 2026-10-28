using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Powers;

public class PietyPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    // At the late end of the turn, reduce the Piety of this power stack by half.
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Enemy && !CombatManager.Instance.IsOverOrEnding)
        {
            if (!Owner.HasPower<RepentancePower>() && !Owner.HasPower<EnlightenedPower>())
            {
                decimal losePiety = -Math.Ceiling(Amount / 2M);
                if (Owner.HasPower<HolySoulPower>())
                {
                    losePiety += Owner.GetPowerAmount<HolySoulPower>();
                }

                if (losePiety > 0)
                {
                    return;
                }

                await PowerCmd.ModifyAmount(choiceContext, this, losePiety, Owner, null);
            }
            else if (Owner.HasPower<RepentancePower>())
            {
                await PowerCmd.ModifyAmount(choiceContext, this, Amount, Owner, null);
            }
        }
    }

    public void OkayGoAheadAndFlash()
    {
        Flash();
    }

    // public static async Task PietyKill(IReadOnlyList<Creature> creatures)
    // {
    //     if (creatures.Count == 0)
    //     {
    //         return;
    //     }
    //
    //     int totalPiety = 0;
    //     // ICombatState combatState = creatures.First().CombatState;
    //     foreach (Creature creature in creatures)
    //     {
    //         // await PlayVfx(creature);
    //         totalPiety += creature.CurrentHp;
    //         await CreatureCmd.Kill(creature);
    //     }
    //     // await Hook.AfterDiedToDoom(combatState, creatures);
    //     
    // }
    
    // public static IReadOnlyList<Creature> GetPiousCreatures(IReadOnlyList<Creature> creatures, int amount)
    // {
    //     List<Creature> pList = new List<Creature>();
    //     List<Creature> cList = creatures.OrderBy((Func<Creature, int>) (c => c.CurrentHp)).ToList();
    //     foreach (Creature c in cList)
    //     {
    //         if (c.CurrentHp < amount)
    //         {
    //             pList.Add(c);
    //             amount -= c.CurrentHp;
    //         }
    //     }
    //     return pList;
    // }
    //
    // public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    // {
    //     if (side != CombatSide.Enemy && ShouldPietyTrigger(participants))
    //     {
    //         await PietyKill(GetPiousCreatures(CombatState.GetCreaturesOnSide(CombatSide.Enemy), Amount));
    //     }
    // }
    //
    // private bool ShouldPietyTrigger(IEnumerable<Creature> participants)
    // {
    //     if (CombatManager.Instance.IsOverOrEnding)
    //     {
    //         return false;
    //     }
    //     if (!participants.Contains(Owner))
    //     {
    //         return false;
    //     }
    //     if (Owner.IsDead)
    //     {
    //         return false;
    //     }
    //     return true;
    // }
    //
    // private static async Task PlayVfx(Creature creature)
    // {
    //     NCreature nCreature = NCombatRoom.Instance?.GetCreatureNode(creature);
    //     if (nCreature == null)
    //     {
    //         return;
    //     }
    //     bool flag = false;
    //     if (creature.IsMonster)
    //     {
    //         flag = Hook.ShouldDie(creature.Player?.RunState ?? creature.CombatState.RunState, creature.CombatState, creature, out AbstractModel _) && creature.Monster.ShouldDisappearFromDoom;
    //     }
    //     List<Creature> source = (from c in creature.CombatState.GetTeammatesOf(creature)
    //         where c.IsAlive
    //         select c).ToList();
    //     if (flag)
    //     {
    //         if (source.Count() != 1 || source.First() != creature)
    //         {
    //             await Cmd.Wait(0.25f);
    //         }
    //         else
    //         {
    //             await Cmd.Wait(1.5f);
    //         }
    //     }
    // }
}