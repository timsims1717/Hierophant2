using BaseLib.Abstracts;
using Hierophant2.Hierophant2Code.Cards.Ancient;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Hierophant2.Hierophant2Code.Cards.Basic;

public class Chastise : Hierophant2Card, ITranscendenceCard
{
    public Chastise() : base(2,
    CardType.Attack,
    CardRarity.Basic,
    TargetType.AnyEnemy)
    {
        WithDamage(8, 2);
        WithPiety(8, 2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);
        await PowerCmd.Apply<PietyPower>(choiceContext, Owner.Creature, DynamicVars[nameof(PietyPower)].BaseValue, Owner.Creature, this);
    }

    public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<Punish>();
}