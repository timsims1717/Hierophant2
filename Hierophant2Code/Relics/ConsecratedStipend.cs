using Hierophant2.Hierophant2Code.Hooks;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Relics;

public class ConsecratedStipend : Hierophant2Relic, IModifyLoseGold
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    public int Count;
    public int Amount = 8;

    public async override Task BeforeCombatStart()
    {
        Count = 3;
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