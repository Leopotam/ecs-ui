// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;

namespace Leopotam.Ecs.Ui.Components {
    public struct EcsUiDropEvent {
        public string WidgetName;
        public GameObject Sender;
        public PointerEventData.InputButton Button;
    }
}