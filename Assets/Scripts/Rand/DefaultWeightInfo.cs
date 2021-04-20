namespace Ruoran.Roguelike.Rand
{
    public class DefaultWeightInfo
    {
        public string Type { get; set; } = "Default";
        public double Weight { get; set; } = 1;
        public double HitL { get; set; } = 0;
        public double HitR { get; set; } = 0;

        public DefaultWeightInfo(string _type, double _weight, double _hitL, double _hitR)
        {
            Type = _type;
            Weight = _weight;
            HitL = _hitL;
            HitR = _hitR;
        }
    }
}