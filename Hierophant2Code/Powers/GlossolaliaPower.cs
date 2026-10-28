using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Powers;

public class GlossolaliaPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    // protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip>(HoverTipFactory.FromPower<FervorPower>());

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        Flash();
        PowerCmd.Apply<FervorPower>(choiceContext, 
            Owner,
            Amount * card.EnergyCost.GetAmountToSpend(), 
            Owner, null);
    }
}