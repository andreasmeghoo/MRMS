using System.ComponentModel.DataAnnotations;

namespace MRMS.Enums
{
    public enum DoseStrengthUnits
    {
        [Display(Name = "%")]
        percent,
        [Display(Name = "mg")]
        milligrams,
        [Display(Name = "μg")]
        micrograms,
        [Display(Name = "mg/mL")]
        milligramsPerMl,
        [Display(Name = "μg/mL")]
        microgramsPerMl
    }
}
