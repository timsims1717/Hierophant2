using BaseLib.Abstracts;
using BaseLib.Hooks;
using Godot;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace Hierophant2.Hierophant2Code.Character;

public class PietySingleton() : CustomSingletonModel(HookType.Combat), IHealthBarForecastSource
{
    public static readonly Color PietyColor = new("FFDE00");
    
    public override async Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (side == CombatSide.Player && combatState.RoundNumber == 1)
        {
            HealthBarForecastRegistry.Register(Hierophant2.CharacterId, "piety-singleton", this);
        }
    }

    public override async Task AfterCombatVictory(CombatRoom room)
    {
        HealthBarForecastRegistry.Unregister(Hierophant2.CharacterId, "piety-singleton");
    }

    public virtual IEnumerable<HealthBarForecastSegment> GetHealthBarForecastSegments(
        HealthBarForecastContext context)
    {
        List<HealthBarForecastSegment> list = new List<HealthBarForecastSegment>();
        if (context.Creature.IsEnemy)
        {
            int pietyAmount = (int) GetCreaturePietyAmount(context.Creature);
            if (pietyAmount > 0)
            {
                HealthBarForecastSegment segment = new HealthBarForecastSegment(
                    pietyAmount, PietyColor, HealthBarForecastDirection.FromLeft,
                    0, null, null, HealthBarForecastLeftOriginLayout.OverlapFromOrigin,
                    0, true
                    );
                list.Add(segment);
            }
        }

        return list;
    }

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Enemy && !CombatManager.Instance.IsOverOrEnding)
        {
            int totalPiety = TotalPiety(participants);
            if (totalPiety > 0)
            {
                // Log.Info("Total Piety: " + totalPiety);
                IReadOnlyList<Creature> piousCreatures = GetPiousCreatures(participants.First().CombatState.Enemies, totalPiety);
                foreach (Creature creature in participants)
                {
                    if (!creature.IsDead && creature.HasPower<PietyPower>())
                    {
                        creature.GetPower<PietyPower>().OkayGoAheadAndFlash();
                    }
                }
                await PietyKill(choiceContext, piousCreatures, participants, totalPiety);
            }
        }
    }

    public static int TotalPiety(IEnumerable<Creature> participants)
    {
        int tp = 0;
        foreach (Creature creature in participants)
        {
            if (!creature.IsDead && creature.HasPower<PietyPower>())
            {
                tp += creature.GetPowerAmount<PietyPower>();
            }
        }

        return tp;
    }
    
    public static async Task PietyKill(PlayerChoiceContext choiceContext, IEnumerable<Creature> enemies, IEnumerable<Creature> players, int totalPiety)
    {
        if (enemies.Count() == 0)
        {
            return;
        }

        // int totalPiety = 0;
        // ICombatState combatState = creatures.First().CombatState;
        foreach (Creature creature in enemies)
        {
            // await PlayVfx(creature);
            int enemyHp = creature.CurrentHp;
            await CreatureCmd.Kill(creature);
            await DecreasePiety(choiceContext, players, enemyHp, totalPiety);
            totalPiety -= enemyHp;
        }
        // await Hook.AfterDiedToDoom(combatState, creatures);
        
    }

    public static decimal GetCreaturePietyAmount(Creature theCreature)
    {
        if (theCreature.CombatState == null)
        {
            return 0;
        }
        decimal amount = TotalPiety(theCreature.CombatState.Creatures);
        List<Creature> cList = theCreature.CombatState.Enemies
            .OrderBy(c => c.CurrentHp).ToList();
        foreach (Creature c in cList)
        {
            if (c == theCreature)
            {
                return Math.Min(amount, c.CurrentHp);
            }
            amount -= c.CurrentHp;
            if (amount <= 0)
            {
                return 0;
            }
        }
        return 0;
    }
    
    public static IReadOnlyList<Creature> GetPiousCreatures(IEnumerable<Creature> creatures, int amount)
    {
        List<Creature> pList = new List<Creature>();
        List<Creature> cList = creatures.Where(c => c.IsEnemy)
            .OrderBy((Func<Creature, int>) (c => c.CurrentHp)).ToList();
        foreach (Creature c in cList)
        {
            if (c.CurrentHp <= amount)
            {
                pList.Add(c);
                amount -= c.CurrentHp;
            }
        }
        return pList;
    }

    public async static Task DecreasePiety(PlayerChoiceContext choiceContext, IEnumerable<Creature> creatures, int pietyToSpend, int totalPiety)
    {
        decimal perc = (decimal) pietyToSpend / totalPiety;
        foreach (Creature creature in creatures.Where(c => !c.IsDead && c.HasPower<PietyPower>() && c.IsPlayer))
        {
            int toLose = (int)(creature.GetPowerAmount<PietyPower>() * perc);
            pietyToSpend -= toLose;
            await PowerCmd.ModifyAmount(choiceContext, creature.GetPower<PietyPower>(), -toLose, null, null);
        }

        while (pietyToSpend > 0)
        {
            foreach (Creature creature in creatures
                 .Where(c => !c.IsDead && c.HasPower<PietyPower>() && c.IsPlayer)
                 .OrderByDescending((Func<Creature, int>) (c => c.GetPowerAmount<PietyPower>())))
            {
                await PowerCmd.Decrement(creature.GetPower<PietyPower>());
                pietyToSpend--;
                if (pietyToSpend <= 0)
                {
                    return;
                }
            }
        }
    }
}