using BaseLib.Abstracts;
using Hierophant2.Hierophant2Code.Hooks;
using Hierophant2.Hierophant2Code.Patches;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Character;

public class GoldHistorySingleton() : CustomSingletonModel(HookType.Combat), IAfterLoseGold
{
    public static List<GoldLostEntry> Entries = new List<GoldLostEntry>();
    
    public async Task AfterLoseGold(
        PlayerChoiceContext choiceContext,
        Player player,
        decimal amount,
        GoldLossType lossType)
    {
        Entries.Add(new GoldLostEntry(player, amount, lossType,
            player.Creature.CombatState.RoundNumber,
            player.Creature.CombatState.CurrentSide,
            CombatManager.Instance.History,
            player.Creature.CombatState.Players));
    }

    public static void Clear()
    {
        Entries.Clear();
    }
}