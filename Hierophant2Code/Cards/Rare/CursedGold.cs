using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class CursedGold : Hierophant2Card
{
    public CursedGold() : base(1,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithGilded();
        UpgradeKeywords.Add((CardKeyword.Innate, UpgradeType.Add));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<CursedGoldPower>(choiceContext, 
            Owner.Creature,
            1, 
            Owner.Creature, this);
    }
}