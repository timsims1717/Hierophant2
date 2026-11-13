using BaseLib.Extensions;
using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Idol : Hierophant2Card
{
    public Idol() : base(-1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithBlock(5, 2);
        WithPiety(5, 2);
    }
    
    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int xValue = ResolveEnergyXValue();
        for (int i = 0; i < xValue; i++)
        {
            await CommonActions.CardBlock(this, play);
            await PowerCmd.Apply<PietyPower>(choiceContext,
                Owner.Creature,
                DynamicVars.Power<PietyPower>().IntValue,
                Owner.Creature,
                this);
        }
    }
}