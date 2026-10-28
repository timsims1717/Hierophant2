using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class Purify : Hierophant2Card
{
    public Purify() : base(2,
        CardType.Attack,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithKeyword(Tithe);
        WithDamage(12, 4);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        List<CardModel> list = PileType.Hand.GetPile(Owner).Cards.ToList();
        foreach (CardModel card in list)
        {
            if (card.Type == CardType.Curse || card.Type == CardType.Status)
            {
                await CardCmd.Exhaust(choiceContext, card);
            }
        }
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);
    }
}