using UnityEngine;
using UnityEngine.UI;

namespace Torappu.UI
{
	public class UIImageFastRectMask : BaseMeshEffect
	{
		[SerializeField]
		private RectTransform _bestFitRect;

        public override void ModifyMesh(VertexHelper vh)
        {
            throw new System.NotImplementedException();
        }
    }
}
