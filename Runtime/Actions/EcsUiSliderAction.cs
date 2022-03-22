// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing Slider events.
    /// </summary>
    [RequireComponent (typeof (Slider))]
    public sealed class EcsUiSliderAction : EcsUiActionBase {
        Slider _slider;

        void Awake () {
            _slider = GetComponent<Slider> ();
            _slider.onValueChanged.AddListener (OnSliderValueChanged);
        }

        void OnSliderValueChanged (float value) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiSliderChangeEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = _slider;
                msg.Value = value;
            }
        }
    }
}