namespace Ruoran.Roguelike.Rand
{
    public class RoomChunkWeightInfo : DefaultWeightInfo
    {
        public int X { get; set; } = 1;
        public int Y { get; set; } = 1;

        public RoomChunkWeightInfo(int _x, int _y, string _type, double _weight, double _hitL, double _hitR) : base(_type, _weight, _hitL, _hitR)
        {
            X = _x;
            Y = _y;
        }
    }
}