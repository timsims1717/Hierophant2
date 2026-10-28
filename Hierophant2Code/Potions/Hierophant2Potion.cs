using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Character;
using Hierophant2.Hierophant2Code.Extensions;

namespace Hierophant2.Hierophant2Code.Potions;

[Pool(typeof(Hierophant2PotionPool))]
public abstract class Hierophant2Potion : CustomPotionModel
{
    public override string? CustomPackedImagePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();

    public override string? CustomPackedOutlinePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionOutlineImagePath();
}