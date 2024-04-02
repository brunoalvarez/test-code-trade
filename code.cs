using System.IO.Pipes;

namespace ApllyTestProject
{
    public interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
    }

    public interface ITradeCategoryStrategy
    {
        string? Categorize(ITrade trade);
    }

    public class Trade : ITrade
    {
        public double Value { get; private set; }
        public string ClientSector { get; private set; }

        public Trade(double value, string clientSector)
        {
            Value = value;
            ClientSector = clientSector;
        }
    }

    public class LowRiskStrategy : ITradeCategoryStrategy
    {
        public string? Categorize(ITrade trade)
        {
            if (trade.Value < 1_000_000 && trade.ClientSector == "Public") return "LOWRISK";
            return null;
        }
    }

    public class MediumRiskStrategy : ITradeCategoryStrategy
    {
        public string? Categorize(ITrade trade)
        {
            if (trade.Value > 1_000_000 && trade.ClientSector == "Public") return "MEDIUMRISK";
            return null;
        }
    }

    public class HighRiskStrategy : ITradeCategoryStrategy
    {
        public string? Categorize(ITrade trade)
        {
            if (trade.Value > 1_000_000 && trade.ClientSector == "Private") return "HIGHRISK";
            return null;
        }
    }

    public class TradeCategorizer
    {
        private readonly List<ITradeCategoryStrategy> _strategies;

        public TradeCategorizer()
        {
            _strategies = new List<ITradeCategoryStrategy>
            {
                new LowRiskStrategy(),
                new MediumRiskStrategy(),
                new HighRiskStrategy()
            };
        }

        public List<string> CategorizeTrades(IEnumerable<ITrade> trades)
        {
            var categories = new List<string>();

            foreach (var trade in trades)
            {
                foreach (var strategy in _strategies)
                {
                    var category = strategy.Categorize(trade);
                    if (category != null)
                    {
                        categories.Add(category);
                        break;
                    }
                }
            }

            return categories;
        }
    }

    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("App Trades:");

            List<ITrade> trades = new List<ITrade>
        {
            new Trade(2_000_000, "Private"),
            new Trade(400_000, "Public"),
            new Trade(500_000, "Public"),
            new Trade(3_000_000, "Public")
        };


            TradeCategorizer categorizer = new();
            List<string> categories = categorizer.CategorizeTrades(trades);

            for (int i = 0; i < trades.Count; i++)
            {
                Console.WriteLine($"Trade {i + 1}: {categories[i]}");
            }
        }
    }
}





