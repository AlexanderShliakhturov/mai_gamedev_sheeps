using System;
using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Grab.Scan;
using App.Scripts.Services._Core.Components.Scan;
using App.Scripts.Services.Input;
using App.Scripts.Services.Input.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Scripts.Services._Core.Components.Grab
{
    [RequireComponent(typeof(ScanComponent))]
    public class GrabComponent : ActivatableObject, IInputObtainable, IInitializable
    {
        public event Action<GameObject> Grabbed;
        [SerializeField] private ScanConfig scanConfig;
        private ScanComponent _scanComponent;

        public GrabbableComponent CurrentGrabbable { get; private set; }
        private GrabbableComponent _currentGrabbableCandidate;

        public UniTask InitAsync()
        {
            scanConfig.Id = Guid.NewGuid();
            scanConfig.ScanProcessor = new GrabScanProcessor();
            _scanComponent = GetComponent<ScanComponent>();
            var subscriber = _scanComponent.Subscribe(scanConfig);
            subscriber.Scanned += OnScanned;
            return UniTask.CompletedTask;
        }

        private void OnScanned(Transform obj) =>
            _currentGrabbableCandidate = obj?.GetComponent<GrabbableComponent>();

        public void AssignInput(InputService inputService)
        {
            inputService.InputActions.Player.Attack.performed += TryGrab;
            inputService.InputActions.Player.Ungrab.performed += Ungrab;
        }

        private void Ungrab(InputAction.CallbackContext obj)
        {
            if (!CurrentGrabbable)
                return;

            CurrentGrabbable.OnReleased();
            CurrentGrabbable = null;
            Grabbed?.Invoke(null);
        }

        private void TryGrab(InputAction.CallbackContext obj)
        {
            if (CurrentGrabbable)
                return;

            CurrentGrabbable = _currentGrabbableCandidate;
            CurrentGrabbable?.OnGrabbed(scanConfig.scanPoint);
            Grabbed?.Invoke(CurrentGrabbable?.gameObject);
        }
    }
}