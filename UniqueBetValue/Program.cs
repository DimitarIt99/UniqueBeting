namespace UniqueBetValue
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using UniqueBetValue.Logger;

    public class Program
    {
        private static List<string> difficulties;
        private static List<decimal> betValues;
        private static List<int> lineCounts;
        private static ILogger logger;
        static void Main(string[] args)
        {
            difficulties = new List<string>();
            betValues = new List<decimal>();
            lineCounts = new List<int>();
            logger = new Logger.Logger();

            var json = File.ReadAllText("./nominals_json.txt");
            GetBetValueCombinatios(json);
        }

        static void GetBetValueCombinatios(string textFile)
        {
            var nominals = JsonConvert.DeserializeObject<List<Nominal>>(textFile);

            foreach (var nominal in nominals)
            {
                //Getting all the unique value for betValue lineCount and difficulty
                if (!difficulties.Contains(nominal.Extra_Info.NominalParams.Difficulty))
                {
                    difficulties.Add(nominal.Extra_Info.NominalParams.Difficulty);
                }
                if (!betValues.Contains(nominal.Extra_Info.NominalParams.BetValue))
                {
                    betValues.Add(nominal.Extra_Info.NominalParams.BetValue);
                }
                if (!lineCounts.Contains(nominal.Extra_Info.NominalParams.LineCount))
                {
                    lineCounts.Add(nominal.Extra_Info.NominalParams.LineCount);
                }
            }
            int lineCountIndex = 0;
            int difficultyIndex = 0;
            var uniqueNominals = new List<NominalParams>();
            foreach (var betValue in betValues)
            {
                var nominal = new NominalParams();
                nominal.BetValue = betValue;
                nominal.Difficulty = difficulties[difficultyIndex];
                nominal.LineCount = lineCounts[lineCountIndex];

                lineCountIndex++;
                //Getting the unique combinations of lineCount and difficulty
                if (lineCountIndex == lineCounts.Count)
                {
                    lineCountIndex = 0;
                    difficultyIndex++;
                }
                if (difficultyIndex == difficulties.Count)
                {
                    difficultyIndex = 0;
                }
                uniqueNominals.Add(nominal);
            }
            foreach (var nominal in uniqueNominals)
            {
                int recno = GetRecnoByNominals(nominals, nominal);
                logger.Log($"{recno}, {nominal.BetValue}, {nominal.Difficulty}, {nominal.LineCount}");
            }

        }

        private static int GetRecnoByNominals(List<Nominal> nominals, NominalParams nominal)
            => nominals.Where(a => a.Extra_Info.NominalParams.BetValue == nominal.BetValue
            && a.Extra_Info.NominalParams.Difficulty == nominal.Difficulty
            && a.Extra_Info.NominalParams.LineCount == nominal.LineCount)
            .Select(a => a.Recno)
            .FirstOrDefault();
    }

}
