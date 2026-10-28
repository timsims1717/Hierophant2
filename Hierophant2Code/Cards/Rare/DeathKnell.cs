using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class DeathKnell : Hierophant2Card
{
    public DeathKnell() : base(1,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithPower<DeathKnellPower>(3, -1);
        WithVar("FervorAmount", 40);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<DeathKnellPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<DeathKnellPower>().IntValue,
            Owner.Creature, this);
    }
}