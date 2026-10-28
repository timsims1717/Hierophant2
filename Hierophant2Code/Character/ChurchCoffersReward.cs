using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace Hierophant2.Hierophant2Code.Character;

public class ChurchCoffersReward : Reward
{
    protected override RewardType RewardType => RewardType.Gold;

    public override int RewardsSetIndex => 1;
    public int Amount { get; private set; } = -1;
    
    private static string RewardIcon
    {
        get => ImageHelper.GetImagePath("ui/reward_screen/reward_icon_money.png");
    }
    
    protected override string IconPath => RewardIcon;
    
    public override Vector2 IconPosition => new Vector2(0.0f, -5f);
    
    public override LocString Description
    {
        get
        {
            LocString description = new LocString("gameplay_ui", "HIEROPHANT_CHURCH_COFFERS_REWARD");
            description.Add("gold", Amount);
            return description;
        }
    }
    
    public ChurchCoffersReward(int amount, Player player)
        : base(player)
    {
        Amount = amount;
    }
    
    public override bool IsPopulated => Amount >= 0;
    
    public override void Populate() {}
    
    protected override async Task<bool> OnSelect()
    {
        await PlayerCmd.GainGold(Amount, Player);
        Log.Info($"Player {Player.NetId} obtained {Amount} gold from the Church Coffers");
        return true;
    }
    
    public override SerializableReward ToSerializable()
    {
        return new SerializableReward
        {
            RewardType = RewardType.Gold,
            GoldAmount = Amount
        };
    }

    public override void MarkContentAsSeen() {}
}