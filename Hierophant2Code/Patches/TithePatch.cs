#region

using HarmonyLib;
using Hierophant2.Hierophant2Code.Cards;
using Hierophant2.Hierophant2Code.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;

#endregion

namespace Hierophant2.Hierophant2Code.Patches;

[HarmonyPatch(typeof(PlayerCombatState),
	nameof(PlayerCombatState.HasEnoughResourcesFor),
	argumentTypes: [typeof(CardModel), typeof(UnplayableReason)],
	[ArgumentType.Normal, ArgumentType.Out])]
internal static class UnplayableTithePatch
{
    [HarmonyPostfix]
    internal static bool Postfix(bool __result, CardModel card, ref UnplayableReason reason, PlayerCombatState __instance)
    {
	    if ((card.Keywords.Contains(Hierophant2Card.Tithe) || Hierophant2Card.HasSingleTurnTithe.Get(card)) && reason == UnplayableReason.EnergyCostTooHigh) {
		    int num = Math.Max(0, card.EnergyCost.GetWithModifiers(CostModifiers.All));
		    int diff = num - __instance.Energy;
		    if (card.Owner.Gold >= diff * Hierophant2Card.TitheAmount)
		    {
			    reason = UnplayableReason.None;
			    return true;
		    }
	    }
	    return __result;
    }
}

[HarmonyPatch(typeof(CardModel),
	nameof(CardModel.SpendResources))]
internal static class SpendResourcedTithePatch
{
	[HarmonyPrefix]
	internal static void Prefix(CardModel __instance)
	{
		if (__instance.Keywords.Contains(Hierophant2Card.Tithe) || Hierophant2Card.HasSingleTurnTithe.Get(__instance)) {
			int energy = __instance.Owner.PlayerCombatState.Energy;
			int energyToSpend = __instance.EnergyCost.GetAmountToSpend();
			int diff = energyToSpend - energy;
			if (diff > 0)
			{
				PlayerCmd.LoseGold(Hierophant2Card.TitheAmount*diff, __instance.Owner,GoldLossType.Spent);
			}
		}
	}
}

[HarmonyPatch(typeof(CardModel),
	nameof(CardModel.EndOfTurnCleanup))]
internal static class EndOfTurnCleanupTithePatch
{
	[HarmonyPrefix]
	internal static void Prefix(CardModel __instance)
	{
		if (Hierophant2Card.HasSingleTurnTithe.Get(__instance))
		{
			__instance.RemoveKeyword(Hierophant2Card.Tithe);
			NCard.FindOnTable(__instance)?.UpdateVisuals(__instance.Pile.Type, CardPreviewMode.Normal);
		}
		Hierophant2Card.HasSingleTurnTithe.Set(__instance, false);
	}
}