using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Hooks;

public interface IModifyLoseGold
{
    Task ModifyLoseGold(
        PlayerChoiceContext choiceContext,
        Player player,
        ref decimal amount,
        GoldLossType lossType);
}