namespace Ruoran.Roguelike.Rand
{
    public static class RNG
    {
        public static long Seed { get; private set; } = 1;

        public static void Reseed(long _seed)
        {
            Seed = _seed;
        }
        public static int Rand()
        {
            Seed *= 16807;
            Seed %= 2147483647;
            return (int)Seed;
        }

        public static double RandDouble(double l = 0, double r = 1)
        {
            return l + Rand() / 2147483647.0 * (r - l);
        }

        public static int RandInt(int l = 0, int r = 1)
        {
            return (int)(l + RandDouble() * (r + 1 - l));
        }
    }
}
