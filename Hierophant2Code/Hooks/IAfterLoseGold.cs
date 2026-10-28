using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Hooks;

public interface IAfterLoseGold
{
    Task AfterLoseGold(
        PlayerChoiceContext choiceContext,
        Player player,
        decimal amount,
        GoldLossType lossType);
}