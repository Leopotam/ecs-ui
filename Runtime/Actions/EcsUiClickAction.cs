// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing OnClick events.
    /// </summary>
    public sealed class EcsUiClickAction : EcsUiActionBase, IPointerClickHandler {
        [Range (1f, 2048f)]
        public float DragTreshold = 5f;

        void IPointerClickHandler.OnPointerClick (PointerEventData eventData) {
            if ((eventData.pressPosition - eventData.position).sqrMagnitude < DragTreshold * DragTreshold) {
                if (IsValidForEvent ()) {
                    ref var msg = ref Emitter.CreateEntity ().Get<EcsUiClickEvent> ();
                    msg.WidgetName = WidgetName;
                    msg.Sender = gameObject;
                    msg.Position = eventData.position;
                    msg.Button = eventData.button;
                }
            }
        }
    }
}