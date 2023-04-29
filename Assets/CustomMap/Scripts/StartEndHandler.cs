#if UNITY_EDITOR
using Torappu.Battle;

namespace CustomMap
{
    public class StartEndHandler : SpecialTileTypeHandler
    {
        public StartEndHandler(string key)
        {
            this.key = key;
        }

        private readonly string key;

        public override void Handle(Tile tile)
        {
            tile._tileKey = key;
            tile._effect = key;
            tile._data.tileKey = key;
        }
    }
}
#endif