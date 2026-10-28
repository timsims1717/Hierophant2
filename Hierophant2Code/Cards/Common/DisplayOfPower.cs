using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class DisplayOfPower : Hierophant2Card
{
    public DisplayOfPower() : base(1,
        CardType.Attack,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithVar(new ExtraDamageVar(5).WithUpgrade(3));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CardSelectorPrefs prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1)
        {
            PretendCardsCanBePlayed = true
        };
        CardModel? card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this)).FirstOrDefault();
        if (card != null)
        {
            await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Top);
            ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
            int amount = card.EnergyCost.GetAmountToSpend() * DynamicVars.ExtraDamage.IntValue;
            await DamageCmd.Attack(amount)
                .FromCard(this, play)
                .Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_blunt")
                .Execute(choiceContext);
        }
    }
}