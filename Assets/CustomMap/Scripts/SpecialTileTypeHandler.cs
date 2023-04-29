#if UNITY_EDITOR
using Torappu.Battle;

namespace CustomMap
{
    public abstract class SpecialTileTypeHandler
    {
        public abstract void Handle(Tile tile);
    }
}
#endif