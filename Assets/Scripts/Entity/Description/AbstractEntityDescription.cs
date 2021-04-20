using System.Collections.Generic;

namespace Ruoran.Roguelike.Entity
{
    public abstract class AbstractEntityDescription
    {
        public Dictionary<string, string> Tag
        {
            get;
            set;
        }
    }
}
