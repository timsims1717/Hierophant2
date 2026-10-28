using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Proselytize : Hierophant2Card
{
    public Proselytize() : base(2,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        UpgradeKeywords.Add((Tithe, UpgradeType.Add));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ProselytizePower>(choiceContext, 
            Owner.Creature,
            1, 
            Owner.Creature, this);
    }
}