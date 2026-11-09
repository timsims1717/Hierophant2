using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Endure : Hierophant2Card
{
    public Endure() : base(1,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.Self)
    {
        WithBlock(9, 3);
        WithCards(1);
        WithTip(CardKeyword.Retain);
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await PowerCmd.Apply<EndurePower>(choiceContext,
            Owner.Creature,
            DynamicVars.Cards.BaseValue,
            Owner.Creature,
            this);
    }
}