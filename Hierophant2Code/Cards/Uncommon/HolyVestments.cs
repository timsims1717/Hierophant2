using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class HolyVestments : Hierophant2Card
{
    public HolyVestments() : base(1,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithCalculatedBlock(4, 2,
            (card, _) => HolyCardsInDeck(card, _),
            ValueProp.Move, 1, 1);
        WithTags(IsHoly);
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }
}