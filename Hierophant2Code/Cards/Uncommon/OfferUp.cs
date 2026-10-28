using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class OfferUp : Hierophant2Card
{
    public OfferUp() : base(0,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithEnergy(2, 1);
        WithVar("Increase", 1);
        WithKeyword(Tithe);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        EnergyCost.AddThisCombat(1);
    }
}