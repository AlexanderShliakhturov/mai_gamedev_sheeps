using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Movement.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Rotation
{
    [RequireComponent(typeof(MovementComponent))]
    public class MovementLookingComponent : ActivatableObject, IInitializable, IDependUpdatable
    {
        private MovementComponent[] _components;

        public UniTask InitAsync()
        {
            _components = GetComponents<MovementComponent>();
            return UniTask.CompletedTask;
        }

        public void UpdateDependently(float deltaTime)
        {
            foreach (var component in _components)
            {
                if (!component.Activated)
                    continue;
                transform.LookAt(transform.position + component.CurrentDirection);
            }
        }
    }
}