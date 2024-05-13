using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRMSTests
{
    public class BloodTestTests
    {
        [Fact]
        public void TestGenerateBloodTestResult()
        {
            //Arrange
            decimal minimum = (decimal)4.5;
            decimal maximum = 9;
            decimal result;

            //Act
            Random random = new Random();
            result =  (decimal)(random.NextDouble() * (double)(maximum - minimum)) + minimum;

            //Assert
            Assert.True(result <= maximum);
            Assert.True(result >= minimum);
        }
    }
}
