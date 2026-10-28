using Hierophant2.Hierophant2Code.Hooks;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Relics;

public class HallowedStipend : Hierophant2Relic, IModifyLoseGold
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    public int Count;
    public int Amount = 5;
    
    public override RelicModel GetUpgradeReplacement() => ModelDb.Relic<ConsecratedStipend>();

    public async override Task BeforeCombatStart()
    {
        Count = 1;
    }

    public Task ModifyLoseGold(
        PlayerChoiceContext choiceContext,
        Player player,
        ref decimal amount,
        GoldLossType lossType)
    {
        if (Count > 0)
        {
            Flash();
            amount -= Amount;
            Count--;
        }

        return Task.CompletedTask;
    }
}