// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using TMPro;

namespace Leopotam.Ecs.Ui.Components {
    public struct EcsUiTmpDropdownChangeEvent {
        public string WidgetName;
        public TMP_Dropdown Sender;
        public int Value;
    }
}