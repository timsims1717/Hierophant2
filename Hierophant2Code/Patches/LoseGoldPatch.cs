using HarmonyLib;
using Hierophant2.Hierophant2Code.Character;
using Hierophant2.Hierophant2Code.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Patches;

[HarmonyPatch(typeof(PlayerCmd),
    nameof(PlayerCmd.LoseGold),
    argumentTypes: [typeof(Decimal), typeof(Player), typeof(GoldLossType)])]
internal static class ModifyLoseGoldPatch
{
    [HarmonyPrefix]
    internal static void Prefix(ref Decimal amount, Player player, GoldLossType goldLossType)
    {
        if (player is { PlayerCombatState: not null, Creature.CombatState: not null })
        {
            PlayerChoiceContext context = new HookPlayerChoiceContext(player, player.NetId, GameActionType.Combat);
            amount = HierophantHooks.ModifyLoseGold(context, player, amount, goldLossType);
        }
    }
}

[HarmonyPatch(typeof(PlayerCmd),
    nameof(PlayerCmd.LoseGold),
    argumentTypes: [typeof(Decimal), typeof(Player), typeof(GoldLossType)])]
internal static class LoseGoldPatch
{
    [HarmonyPostfix]
    internal static void Postfix(Decimal amount, Player player, GoldLossType goldLossType)
    {
        if (player is { PlayerCombatState: not null, Creature.CombatState: not null })
        {
            PlayerChoiceContext context = new HookPlayerChoiceContext(player, player.NetId, GameActionType.Combat);
            HierophantHooks.AfterLoseGold(context, player, amount, goldLossType);
        }
    }
}

[HarmonyPatch(typeof(CombatHistory),
    nameof(CombatHistory.Clear))]
internal static class ClearGoldHistoryPatch
{
    [HarmonyPostfix]
    internal static void Postfix()
    {
        GoldHistorySingleton.Clear();
    }
}