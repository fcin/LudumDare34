namespace Assets.Scripts
{
    public class Range
    {
        public Range(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }

        public int Min { get; set; }
        public int Max { get; set; }
    }
}
