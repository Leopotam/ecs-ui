// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.Ecs.Ui.Systems {
    /// <summary>
    /// Emitter system for uGui events to ECS world.
    /// </summary>
    public class EcsUiEmitter : MonoBehaviour {
        EcsWorld _world;
        readonly Dictionary<int, GameObject> _actions = new Dictionary<int, GameObject> (64);

        internal virtual void SetWorld (EcsWorld world) {
#if DEBUG
            if (_world != null) { throw new Exception ("World already attached."); }
#endif
            _world = world;
        }

        /// <summary>
        /// Gets attached after InjectUi() call world instance.
        /// </summary>
        public virtual EcsWorld GetWorld () {
            return _world;
        }

        /// <summary>
        /// Creates ecs entity for message.
        /// </summary>
        public virtual EcsEntity CreateEntity () {
            ValidateEcsFields ();
            return _world.NewEntity ();
        }

        /// <summary>
        /// Sets link to named GameObject to use it later from code. If GameObject is null - unset named link.
        /// </summary>
        /// <param name="widgetName">Logical name.</param>
        /// <param name="go">GameObject link.</param>
        public virtual void SetNamedObject (string widgetName, GameObject go) {
            if (!string.IsNullOrEmpty (widgetName)) {
                var id = widgetName.GetHashCode ();
                if (_actions.ContainsKey (id)) {
                    if (!go) {
                        _actions.Remove (id);
                    } else {
                        throw new Exception ($"Action with \"{widgetName}\" name already registered");
                    }
                } else {
                    if ((object) go != null) {
                        _actions[id] = go.gameObject;
                    }
                }
            }
        }

        /// <summary>
        /// Gets link to named GameObject to use it later from code.
        /// </summary>
        /// <param name="widgetName">Logical name.</param>
        public virtual GameObject GetNamedObject (string widgetName) {
            _actions.TryGetValue (widgetName.GetHashCode (), out var retVal);
            return retVal;
        }

        [System.Diagnostics.Conditional ("DEBUG")]
        void ValidateEcsFields () {
#if DEBUG
            if (_world == null) {
                throw new Exception ("[EcsUiEmitter] Call EcsSystems.InjectUi() first.");
            }
#endif
        }
    }
}