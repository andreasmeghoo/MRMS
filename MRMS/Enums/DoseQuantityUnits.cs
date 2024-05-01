using System.ComponentModel.DataAnnotations;

namespace MRMS.Enums
{
    public enum DoseQuantityUnits
    {
        tablets,
        [Display(Name = "mL")]
        millilitres,
        [Display(Name = "g")]
        grams,
        drops,
        injections
    }
}
