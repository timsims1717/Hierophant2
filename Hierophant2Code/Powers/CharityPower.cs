using Hierophant2.Hierophant2Code.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Powers;

public class CharityPower : Hierophant2Power, IAfterLoseGold
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
        await Trigger(choiceContext);
    }
    
    public async Task Trigger(PlayerChoiceContext choiceContext)
    {
        Flash();
        await PowerCmd.Apply<PietyPower>(choiceContext, Owner, Amount, Owner, null);
    }
}