using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Hierophant2.Hierophant2Code.Cards.Common;

public class CoinFling : Hierophant2Card
{
    public CoinFling() : base(0,
        CardType.Attack,
        CardRarity.Common,
        TargetType.AnyEnemy)
    {
        WithGilded();
        WithDamage(9, 3);
    }
    
    protected override bool ShouldGlowGoldInternal => PlayedDoubloonThisTurn(this);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (PlayedDoubloonThisTurn(play.Card))
        {
            ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this, play)
                .Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_blunt")
                .Execute(choiceContext);
        }
    }
}