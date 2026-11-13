using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Chorus : Hierophant2Card
{
    public Chorus() : base(0,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithFervor(3, 2);
        WithPiety(3, 2);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FervorPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<FervorPower>().IntValue,
            Owner.Creature, this);
        await PowerCmd.Apply<PietyPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<PietyPower>().IntValue,
            Owner.Creature, this);
    }
}