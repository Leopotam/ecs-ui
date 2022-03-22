// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Widgets {
    /// <summary>
    /// Non visual interactive Ui widget, keep fillrate / no rendering / alpha-blending.
    /// </summary>
    [RequireComponent (typeof (CanvasRenderer))]
    [RequireComponent (typeof (RectTransform))]
    public class EcsUiNonVisualWidget : Graphic {
        public override void SetMaterialDirty () { }
        public override void SetVerticesDirty () { }
        public override Material material { get { return defaultMaterial; } set { } }
        public override void Rebuild (CanvasUpdate update) { }
    }
}