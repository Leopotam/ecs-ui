// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing enter / exit cursor events.
    /// </summary>
    public sealed class EcsUiEnterExitAction : EcsUiActionBase, IPointerEnterHandler, IPointerExitHandler {
        void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiEnterEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
            }
        }

        void IPointerExitHandler.OnPointerExit (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiExitEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
            }
        }
    }
}