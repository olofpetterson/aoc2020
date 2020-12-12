namespace AoC2020.Solutions
{
    public abstract class BaseDay
    {
        protected string[] lines;

        protected string data;

        protected BaseDay()
        {
            data = System.IO.File.ReadAllText($@"Data\{GetType().Name}.txt");
            lines = System.IO.File.ReadAllLines($@"Data\{GetType().Name}.txt");
        }

        public abstract string SolveA();

        public abstract string SolveB();
    }
}