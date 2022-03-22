// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using TMPro;

namespace Leopotam.Ecs.Ui.Components {
    public struct EcsUiTmpInputEndEvent {
        public string WidgetName;
        public TMP_InputField Sender;
        public string Value;
    }
}