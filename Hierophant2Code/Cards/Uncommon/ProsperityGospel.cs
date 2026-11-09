using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class ProsperityGospel : Hierophant2Card
{
    public ProsperityGospel() : base(0,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithPower<ProsperityGospelPower>(3, 1);
        WithGilded();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ProsperityGospelPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<ProsperityGospelPower>().IntValue,
            Owner.Creature,
            this);
    }
}