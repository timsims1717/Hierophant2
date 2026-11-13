using Hierophant2.Hierophant2Code.Cards.Token;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class Indulgence : Hierophant2Card
{
    public Indulgence() : base(1,
        CardType.Attack,
        CardRarity.Rare,
        TargetType.AnyEnemy)
    {
        WithDamage(10, 4);
        WithKeyword(CardKeyword.Exhaust);
        WithTip(StaticHoverTip.Fatal);
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        bool shouldTriggerFatal = play.Target.Powers.All(p => p.ShouldOwnerDeathTriggerFatal());
        AttackCommand attackCommand = await DamageCmd.Attack(DynamicVars.Damage.IntValue)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
        if (shouldTriggerFatal && attackCommand.Results.SelectMany((r => r)).Any((Func<DamageResult, bool>) (r => r.WasTargetKilled)))
        {
            CardModel newCard = play.Player.RunState.CreateCard<Doubloon>(play.Player);
            CardCmd.Upgrade(newCard);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(newCard, PileType.Deck), 0.5f);
        }
    }
}