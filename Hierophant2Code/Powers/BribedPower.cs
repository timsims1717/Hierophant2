using BaseLib.Abstracts;
using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Cards.Uncommon;
using Hierophant2.Hierophant2Code.Extensions;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Hierophant2.Hierophant2Code.Powers;

public class BribedPower : CustomTemporaryPowerModelWrapper<Bribe, StrengthPower>
{
    protected override bool InvertInternalPowerAmount => true;
    
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();

    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}