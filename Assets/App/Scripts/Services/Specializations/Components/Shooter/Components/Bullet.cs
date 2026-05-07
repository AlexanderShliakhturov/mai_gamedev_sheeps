using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Health;
using App.Scripts.Services._Core.Components.Movement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Specializations.Components.Shooter.Components
{
    [RequireComponent(typeof(TargetMovementComponent))]
    public class Bullet : ActivatableObject, IInitializable
    {
        private TargetMovementComponent _targetMovementComponent;

        public UniTask InitAsync()
        {
            _targetMovementComponent = GetComponent<TargetMovementComponent>();
            _targetMovementComponent.InitAsync();
            return UniTask.CompletedTask;
        }

        public void Fire(Vector3 position) => 
            _targetMovementComponent.Move(position - transform.position, 1f);

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out HealthComponent health))
                return;
            
            health.Damage(1);
        }
    }
}