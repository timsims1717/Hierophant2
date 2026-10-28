using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class RepressedViolence : Hierophant2Card
{
    private const int _baseDamage = 1;
    private int _currentDamage = 1;
    private int _increasedDamage;
    
    public RepressedViolence() : base(0,
        CardType.Attack,
        CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        WithDamage(CurrentDamage);
        WithVar("DamageIncrease", 3, 1);
        WithKeyword(CardKeyword.Ethereal);
        WithTip(CardKeyword.Exhaust);
    }
    
    [SavedProperty]
    public int CurrentDamage
    {
        get => _currentDamage;
        set
        {
            AssertMutable();
            _currentDamage = value;
            DynamicVars.Damage.BaseValue = _currentDamage;
        }
    }
    
    [SavedProperty]
    public int IncreasedDamage
    {
        get => _increasedDamage;
        set
        {
            AssertMutable();
            _increasedDamage = value;
        }
    }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext,
        CardModel card,
        bool causedByEthereal)
    {
        if (card == this)
        {
            int increase = DynamicVars["DamageIncrease"].IntValue;
            BuffFromExhaust(increase);
            if (!(DeckVersion is RepressedViolence deckVersion))
            {
                return;
            }

            deckVersion.BuffFromExhaust(increase);
        }
    }
    
    private void BuffFromExhaust(int extraDamage)
    {
        IncreasedDamage += extraDamage;
        UpdateDamage();
    }
    
    private void UpdateDamage() => CurrentDamage = _baseDamage + IncreasedDamage;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt")
            .Execute(choiceContext);
    }
}