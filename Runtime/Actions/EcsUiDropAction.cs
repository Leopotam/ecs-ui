// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing OnDrop events.
    /// </summary>
    public sealed class EcsUiDropAction : EcsUiActionBase, IDropHandler {
        void IDropHandler.OnDrop (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiDropEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
                msg.Button = eventData.button;
            }
        }
    }
}