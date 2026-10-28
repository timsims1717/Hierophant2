using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Hierophant2.Hierophant2Code.Powers;

  public class DazzledPower : CustomTemporaryPowerModelWrapper<DazzlePower, StrengthPower>
  {
      protected override bool InvertInternalPowerAmount => true;
  }