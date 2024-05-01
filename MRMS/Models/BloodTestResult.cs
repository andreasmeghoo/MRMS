using MRMS.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Pkcs;

namespace MRMS.Models
{
    public class BloodTestResult
    {
        public int BloodTestResultId { get; set; }
        public int BloodTestId { get; set; }
        [Display(Name="Red Blood Cell Count")]
        public decimal redBloodCellCount { get; set; }
        [Display(Name = "White Blood Cell Count")]
        public decimal whiteBloodCellCount { get; set; }
        [Display(Name = "Platelet Count")]
        public decimal plateletCount { get; set; }
        [Display(Name = "Glucose Level")]
        public decimal glucoseLevel { get; set; }
        [Display(Name = "Cholesterol Level")]
        public decimal cholestorolLevel { get; set; }
        [Display(Name = "Liver Function")]
        public decimal liverFunction {  get; set; }
        [Display(Name = "Kidney Function")]
        public decimal kidneyFunction { get; set; }

       
    }
}
