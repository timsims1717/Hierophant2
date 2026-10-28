using Hierophant2.Hierophant2Code.Cards;
using Hierophant2.Hierophant2Code.Character;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace Hierophant2.Hierophant2Code.Powers;

public class ChurchCoffersPower : Hierophant2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player)
        {
            return;
        }

        foreach (CardModel card in await CardSelectCmd.FromHand(choiceContext, player,
                     new CardSelectorPrefs(SelectionScreenPrompt, Amount) { PretendCardsCanBePlayed = true },
                     Hierophant2Card.CanHaveTithe, this))
        {
            card.AddKeyword(Hierophant2Card.Tithe);
            NCard.FindOnTable(card)?.UpdateVisuals(card.Pile.Type, CardPreviewMode.Normal);
        }
    }
}