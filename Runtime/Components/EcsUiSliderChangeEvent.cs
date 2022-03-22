// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Components {
    public struct EcsUiSliderChangeEvent {
        public string WidgetName;
        public Slider Sender;
        public float Value;
    }
}