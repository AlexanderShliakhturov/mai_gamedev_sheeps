using System.Collections.Generic;
using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Movement.Base;
using App.Scripts.Services.Chunks.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Entities.Components.Base
{
    public abstract class Entity : ActivatableObject, IDependUpdatable, IResetable
    {
        public ChunkComponent Chunk;

        private List<IDependUpdatable> _dependUpdatables;

        public async UniTask InitAsync()
        {
            _dependUpdatables = new();
            foreach (var initializable in GetComponents<IInitializable>())
                await initializable.InitAsync();

            foreach (var dependUpdatable in GetComponents<IDependUpdatable>())
            {
                if (dependUpdatable as Entity == this)
                    continue;
                _dependUpdatables.Add(dependUpdatable);
            }
        }

        public void AddUpdatable(IDependUpdatable dependUpdatable) => _dependUpdatables.Add(dependUpdatable);

        public virtual void UpdateDependently(float deltaTime)
        {
            if (!Activated)
                return;

            foreach (var updatable in _dependUpdatables)
                updatable.UpdateDependently(deltaTime);
        }

        protected override void ActivateInternal()
        {
            foreach (var activatableObject in GetComponents<ActivatableObject>())
            {
                if (activatableObject as Entity == this)
                    continue;

                activatableObject.Activate();
            }
        }

        protected override void DeactivateInternal()
        {
            foreach (var activatableObject in GetComponents<ActivatableObject>())
            {
                if (activatableObject as Entity == this)
                    continue;

                activatableObject.Deactivate();
            }
        }

        public void Reset()
        {
            foreach (var resetable in GetComponents<IResetable>())
            {
                if (resetable as Entity == this)
                    continue;

                resetable.Reset();
            }
        }

        public void ToggleMotion(bool b)
        {
            foreach (var component in GetComponents<MovementComponent>())
            {
                if (b)
                    component.Activate();
                else
                    component.Deactivate();
            }
        }

        public void TogglePhysics(bool b)
        {
            var body = GetComponent<Rigidbody>();
            if (!b)
            {
                body.linearVelocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
            }

            body.isKinematic = !b;
        }
    }
}