using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Rooms;

namespace Hierophant2.Hierophant2Code.Character;

public class HoardSingleton() : CustomSingletonModel(HookType.Combat)
{
    public override Task AfterCombatEnd(CombatRoom room)
    {
        foreach (Player player in room.CombatState.Players)
        {
            int goldAmt = (
                from pile in player.Piles
                where pile.Type == PileType.Discard || pile.Type == PileType.Draw || pile.Type == PileType.Hand
                    from card in pile.Cards
                    where card.DynamicVars.Keys.Contains(HoardVar.DefaultName) && card.DynamicVars[HoardVar.DefaultName].BaseValue > 0
                    select card.DynamicVars[HoardVar.DefaultName].IntValue
            ).Sum();

            if (goldAmt > 0)
            {
                room.AddExtraReward(player, new HoardReward(goldAmt, player));
            }
        }

        return Task.CompletedTask;
    }
}