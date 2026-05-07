using App.Scripts.Services._Core.Components._Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Movement.Base
{
    public abstract class MovementComponent : ActivatableObject, IInitializable
    {
        [SerializeField] private float maxVelocity = 7;
        [SerializeField] protected float speed;
        [SerializeField] private ForceMode forceMode;
        protected Rigidbody Rigidbody;
        public Vector3 CurrentDirection { get; private set; }

        public virtual UniTask InitAsync()
        {
            Rigidbody = GetComponent<Rigidbody>();
            return UniTask.CompletedTask;
        }

        public void Move(Vector3 horizontalDirection, float deltaTime)
        {
            CurrentDirection = horizontalDirection;
            Rigidbody.AddForce(horizontalDirection.normalized * (speed * deltaTime), forceMode);
            Rigidbody.linearVelocity = Vector3.ClampMagnitude(Rigidbody.linearVelocity, maxVelocity);
        }
    }
}