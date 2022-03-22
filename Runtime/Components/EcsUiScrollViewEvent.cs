// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Components {
    public struct EcsUiScrollViewEvent {
        public string WidgetName;
        public ScrollRect Sender;
        public Vector2 Value;
    }
}