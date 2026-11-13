using BaseLib.Extensions;
using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class Profess : Hierophant2Card
{
    public Profess() : base(1,
        CardType.Skill,
        CardRarity.Common,
        TargetType.Self)
    {
        WithBlock(5, 2);
        WithPiety(5, 2);
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await PowerCmd.Apply<PietyPower>(choiceContext, 
            Owner.Creature,
            DynamicVars.Power<PietyPower>().IntValue, 
            Owner.Creature, 
            this);
    }
}