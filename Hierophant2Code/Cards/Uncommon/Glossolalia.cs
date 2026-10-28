using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Glossolalia : Hierophant2Card
{
    public Glossolalia() : base(2,
        CardType.Power,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<GlossolaliaPower>(choiceContext, 
            Owner.Creature,
            1, 
            Owner.Creature, this);
    }
}