using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;

namespace Hierophant2.Hierophant2Code.Hooks;

public static class HierophantHooks
{
    public static Task AfterLoseGold(PlayerChoiceContext choiceContext, Player player, decimal amount, GoldLossType goldLossType)
    {
        return HookUtils.Dispatch<IAfterLoseGold>(player.Creature.CombatState, m => m.AfterLoseGold(choiceContext, player, amount, goldLossType));
    }
    
    public static decimal ModifyLoseGold(PlayerChoiceContext choiceContext, Player player, decimal amount, GoldLossType goldLossType)
    {
        decimal newAmount = amount;
        HookUtils.Dispatch<IModifyLoseGold>(player.Creature.CombatState, m => m.ModifyLoseGold(choiceContext, player, ref newAmount, goldLossType));
        return newAmount;
    }
}