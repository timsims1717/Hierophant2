using BaseLib.Abstracts;
using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Extensions;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Hierophant2.Hierophant2Code.Powers;

public class HolyStrengthPower : CustomTemporaryPowerModelWrapper<HolinessPower, StrengthPower>
{
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();

    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}