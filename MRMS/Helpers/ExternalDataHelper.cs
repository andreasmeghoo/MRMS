namespace MRMS.Helpers
{
    public class ExternalDataHelper
    {
        public static bool FuzzyMatch(string stringA, string stringB)
        {
            return Distance(stringA, stringB) < 5;
        }

        public static int Distance(string stringA, string stringB)
        {
            int[,] d = new int[stringA.Length + 1, stringB.Length + 1];

            if (stringA.Length == 0)
                return stringB.Length;
            if (stringB.Length == 0)
                return stringA.Length;

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

            return d[stringA.Length, stringB.Length];
        }
    }
}
