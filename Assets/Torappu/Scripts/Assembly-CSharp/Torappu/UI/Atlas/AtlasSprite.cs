using System;

namespace Torappu.UI.Atlas
{
	[Serializable]
	public class AtlasSprite
	{
		public string name;

		public string guid;

		public int atlas;

		public AtlasCoord rect;

		public bool rotate;
	}
}
