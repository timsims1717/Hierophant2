using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class ChurchCoffers : Hierophant2Card
{
    public ChurchCoffers() : base(3,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<ChurchCoffersPower>(1);
        WithKeyword(Tithe);
        WithCostUpgradeBy(-1);
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ChurchCoffersPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<ChurchCoffersPower>().IntValue,
            Owner.Creature,
            this);
    }
}