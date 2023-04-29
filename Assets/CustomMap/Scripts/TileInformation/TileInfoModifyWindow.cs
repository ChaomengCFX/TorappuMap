#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/29

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace CustomMap.TileInformation
{
    public class TileInfoModifyWindow : OdinEditorWindow
    {
        [ShowInInspector, HideLabel]
        public CustomTileInfo.IModifyInterface ModifyInterface { get; set; }
    }
}
#endif