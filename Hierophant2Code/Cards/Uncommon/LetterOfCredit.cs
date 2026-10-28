using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class LetterOfCredit : Hierophant2Card
{
    public LetterOfCredit() : base(0,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.Self)
    {
        WithBlock(3, 2);
        WithGilded();
        WithTitheTip();
    }
    
    public override bool GainsBlock => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        CardSelectorPrefs prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1)
        {
            PretendCardsCanBePlayed = true
        };
        CardModel? card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, CanHaveTithe, this)).FirstOrDefault();
        if (card != null)
        {
            ApplySingleTurnTithe(card);
        }
    }
}