using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Movement.Base;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Movement
{
    public class TargetMovementComponent : MovementComponent, IDependUpdatable
    {
        public Transform currentTarget;
        [SerializeField] private float stopDistance = 1f;
        
        public void UpdateDependently(float deltaTime)
        {
            if (!Activated)
                return;
            
            if (!currentTarget)
                return;
            
            var direction = currentTarget.transform.position - transform.position;
            
            if (direction.magnitude <= stopDistance)
                return;
            
            Move(direction.normalized, deltaTime);
        }
    }
}