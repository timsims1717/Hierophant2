using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class DivineIntervention : Hierophant2Card
{
    public DivineIntervention() : base(3,
        CardType.Power,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<DivineInterventionPower>(20, 5);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<DivineInterventionPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<DivineInterventionPower>().IntValue, 
            Owner.Creature, this);
    }
}