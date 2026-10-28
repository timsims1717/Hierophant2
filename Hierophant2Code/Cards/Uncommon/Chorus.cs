using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Chorus : Hierophant2Card
{
    public Chorus() : base(1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithFervor(3, 0);
        WithKeyword(CardKeyword.Exhaust);
        UpgradeKeywords.Add((CardKeyword.Exhaust, UpgradeType.Remove));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FervorPower>(choiceContext, 
            Owner.Creature,
            DynamicVars[nameof(FervorPower)].BaseValue, 
            Owner.Creature, this);
        await PowerCmd.Apply<FervorPower>(choiceContext, 
            Owner.Creature,
            Owner.Creature.GetPowerAmount<FervorPower>(), 
            Owner.Creature, this);
    }
}