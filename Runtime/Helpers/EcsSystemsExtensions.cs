// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using System;
using System.Reflection;
using Leopotam.Ecs.Ui.Components;
using UnityEngine;

namespace Leopotam.Ecs.Ui.Systems {
    /// <summary>
    /// Marks field of IEcsSystem class to be injected with named UI object.
    /// </summary>
    public sealed class EcsUiNamedAttribute : Attribute {
        public readonly string Name;

        public EcsUiNamedAttribute (string name) {
            Name = name;
        }
    }

    public static class EcsSystemsExtensions {
        /// <summary>
        /// Injects named UI objects and Emitter to all systems added to EcsSystems.
        /// </summary>
        /// <param name="ecsSystems">EcsSystems group.</param>
        /// <param name="emitter">EcsUiEmitter instance.</param>
        /// <param name="skipNoExists">Not throw exception if named action not registered in emitter.</param>
        /// <param name="skipOneFrames">Skip OneFrame-event cleanup registration.</param>
        public static EcsSystems InjectUi (this EcsSystems ecsSystems, EcsUiEmitter emitter, bool skipNoExists = false, bool skipOneFrames = false) {
            if (!skipOneFrames) { InjectOneFrames (ecsSystems); }
            ecsSystems.Inject (emitter);
            if (emitter.GetWorld() == null) { emitter.SetWorld (ecsSystems.World); }
            var uiNamedType = typeof (EcsUiNamedAttribute);
            var goType = typeof (GameObject);
            var componentType = typeof (Component);
            var systems = ecsSystems.GetAllSystems ();
            for (int i = 0, iMax = systems.Count; i < iMax; i++) {
                var system = systems.Items[i];
                if (system is EcsSystems nestedSystems) {
                    nestedSystems.InjectUi(emitter, skipNoExists, skipOneFrames);
                    continue;
                }
                var systemType = system.GetType ();
                foreach (var f in systemType.GetFields (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                    // skip statics or fields without [EcsUiNamed] attribute.
                    if (f.IsStatic || !Attribute.IsDefined (f, uiNamedType)) {
                        continue;
                    }
                    var name = ((EcsUiNamedAttribute) Attribute.GetCustomAttribute (f, uiNamedType)).Name;
#if DEBUG
                    if (string.IsNullOrEmpty (name)) { throw new Exception ($"Cant Inject field \"{f.Name}\" at \"{systemType}\" due to [EcsUiNamed] \"Name\" parameter is invalid."); }
                    if (!(f.FieldType == goType || componentType.IsAssignableFrom (f.FieldType))) {
                        throw new Exception ($"Cant Inject field \"{f.Name}\" at \"{systemType}\" due to [EcsUiNamed] attribute can be applied only to GameObject or Component type.");
                    }
                    if (!skipNoExists && !emitter.GetNamedObject (name)) { throw new Exception ($"Cant Inject field \"{f.Name}\" at \"{systemType}\" due to there is no UI action with name \"{name}\"."); }
#endif
                    var go = emitter.GetNamedObject (name);
                    // GameObject.
                    if (f.FieldType == goType) {
                        f.SetValue (system, go);
                        continue;
                    }
                    // Component.
                    if (componentType.IsAssignableFrom (f.FieldType)) {
                        f.SetValue (system, go != null ? go.GetComponent (f.FieldType) : null);
                    }
                }
            }
            return ecsSystems;
        }

        static void InjectOneFrames (EcsSystems ecsSystems) {
            ecsSystems.OneFrame<EcsUiBeginDragEvent> ();
            ecsSystems.OneFrame<EcsUiDragEvent> ();
            ecsSystems.OneFrame<EcsUiEndDragEvent> ();
            ecsSystems.OneFrame<EcsUiDropEvent> ();
            ecsSystems.OneFrame<EcsUiClickEvent> ();
            ecsSystems.OneFrame<EcsUiDownEvent> ();
            ecsSystems.OneFrame<EcsUiUpEvent> ();
            ecsSystems.OneFrame<EcsUiEnterEvent> ();
            ecsSystems.OneFrame<EcsUiExitEvent> ();
            ecsSystems.OneFrame<EcsUiScrollViewEvent> ();
            ecsSystems.OneFrame<EcsUiSliderChangeEvent> ();
            ecsSystems.OneFrame<EcsUiTmpDropdownChangeEvent> ();
            ecsSystems.OneFrame<EcsUiTmpInputChangeEvent> ();
            ecsSystems.OneFrame<EcsUiTmpInputEndEvent> ();
        }
    }
}