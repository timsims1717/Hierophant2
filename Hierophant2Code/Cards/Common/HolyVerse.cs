using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class HolyVerse : Hierophant2Card
{
    public HolyVerse() : base(1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.Self)
    {
        WithPiety(10, 5);
        WithTags(IsHoly);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<PietyPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<PietyPower>().BaseValue,
            Owner.Creature,
            this);
    }
}