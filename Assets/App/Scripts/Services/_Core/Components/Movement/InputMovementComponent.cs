using App.Scripts.Services._Core.Components.Movement.Base;
using App.Scripts.Services.Input;
using App.Scripts.Services.Input.Components;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class InputMovementComponent : MovementComponent, IInputObtainable
    {

        public void AssignInput(InputService inputService) => inputService.MoveUpdated += MoveHorizontally;

        private void MoveHorizontally(Vector2 input, float deltaTime) => Move(new Vector3(input.x, 0, input.y), deltaTime);
        
        protected override void ActivateInternal() => Rigidbody.isKinematic = false;
        protected override void DeactivateInternal() => Rigidbody.isKinematic = true;
    }
}