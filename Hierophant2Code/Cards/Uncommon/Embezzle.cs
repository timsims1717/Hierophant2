using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Embezzle : Hierophant2Card
{
    public Embezzle() : base(0,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithPower<EmbezzlePower>(40, 10);
        WithKeyword(CardKeyword.Exhaust);
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PlayerCmd.GainGold(DynamicVars.Power<EmbezzlePower>().BaseValue, play.Player);
        await PowerCmd.Apply<EmbezzlePower>(choiceContext,
            Owner.Creature,
            DynamicVars.Power<EmbezzlePower>().BaseValue,
            Owner.Creature,
            this);
    }
}