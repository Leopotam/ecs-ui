// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing ScrollView events.
    /// </summary>
    [RequireComponent (typeof (ScrollRect))]
    public sealed class EcsUiScrollViewAction : EcsUiActionBase {
        ScrollRect _scrollView;

        void Awake () {
            _scrollView = GetComponent<ScrollRect> ();
            _scrollView.onValueChanged.AddListener (OnScrollViewValueChanged);
        }

        void OnScrollViewValueChanged (Vector2 value) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiScrollViewEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = _scrollView;
                msg.Value = value;
            }
        }
    }
}