using Moq;
using MRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRMSTests
{
    public class ExternalDataTests
    {
        [Fact]
        public void TestFuzzyMatch()
        {
            //Arrange
            var stringA = "Brighton";
            var stringB = "Bryton";
            int distance = 999;
            bool result;

            //Act
            int[,] d = new int[stringA.Length + 1, stringB.Length + 1];

            if (stringA.Length == 0)
                distance = stringB.Length;
            if (stringB.Length == 0)
                distance = stringA.Length;

            for (int i = 0; i <= stringA.Length; i++)
                d[i, 0] = i;
            for (int j = 0; j <= stringB.Length; j++)
                d[0, j] = j;

            for (int j = 1; j <= stringB.Length; j++)
            {
                for (int i = 1; i <= stringA.Length; i++)
                {
                    int cost = (stringA[i - 1] == stringB[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            if(distance != 0)
            {
                distance = d[stringA.Length, stringB.Length];
            }

            if(distance < 5)
            {
                result = true;
            }
            else
            {
                result = false;
            }


            //Assert
            Assert.Equal(result, true);

        }
    }
}
