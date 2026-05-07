using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Grab;
using App.Scripts.Services.Input;
using App.Scripts.Services.Input.Components;
using App.Scripts.Services.Specializations.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Scripts.Services.Player.Components.Upgrade
{
    [RequireComponent(typeof(GrabComponent))]
    public class UpgradeComponent : ActivatableObject, IInitializable, IInputObtainable
    {
        private GrabComponent _grabComponent;

        public UniTask InitAsync()
        {
            _grabComponent = GetComponent<GrabComponent>();
            return UniTask.CompletedTask;
        }

        public void AssignInput(InputService inputService) => inputService.InputActions.Player.Interact.performed += TryUpgrade;

        private void TryUpgrade(InputAction.CallbackContext obj)
        {
            if (!_grabComponent.CurrentGrabbable)
                return;
            
            if (!_grabComponent.CurrentGrabbable.TryGetComponent(out SpecializationManagementComponent specialization))
                return;

            if (!specialization.Current)
            {
                specialization.TryChooseSpecialization();
                return;
            }
            
            specialization.TryIncreaseLevel();
        }
    }
}