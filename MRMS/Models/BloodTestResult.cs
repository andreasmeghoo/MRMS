using System.Security.Cryptography.Pkcs;

namespace MRMS.Models
{
    public class BloodTestResult
    {
        public int BloodTestResultId { get; set; }
        public int BloodTestId { get; set; }
        public int redBloodCellCount { get; set; }
        public int whiteBloodCellCount { get; set; }

        public int plateletCount { get; set; }
        public int glucoseLevel { get; set; }
        public int cholestorolLevel { get; set; }
        public int liverFunction {  get; set; }
        public int kidneyFunction { get; set; }

       
    }
}
