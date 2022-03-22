// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using TMPro;
using UnityEngine;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing InputField events.
    /// </summary>
    [RequireComponent (typeof (TMP_Dropdown))]
    public sealed class EcsUiTmpDropdownAction : EcsUiActionBase {
        TMP_Dropdown _dropdown;

        void Awake () {
            _dropdown = GetComponent<TMP_Dropdown> ();
            _dropdown.onValueChanged.AddListener (OnDropdownValueChanged);
        }

        void OnDropdownValueChanged (int value) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiTmpDropdownChangeEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = _dropdown;
                msg.Value = value;
            }
        }
    }
}