using Hierophant2.Hierophant2Code.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;

namespace Hierophant2.Hierophant2Code.Powers;

public class EmbezzlePower : Hierophant2Power, IAfterLoseGold
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public async Task AfterLoseGold(
        PlayerChoiceContext choiceContext,
        Player player,
        decimal amount,
        GoldLossType lossType)
    {
        if (amount >= Amount)
        {
            await PowerCmd.Remove<EmbezzlePower>(Owner);
        }
        else
        {
            await PowerCmd.ModifyAmount(choiceContext, this, -amount, null, null);
        }
    }

    public override async Task AfterCombatEnd(CombatRoom room)
    {
        if (Amount > 0 && Owner.IsPlayer)
        {
            Flash();
            PlayerCmd.LoseGold(Amount, Owner.Player, GoldLossType.None);
        }
    }
}