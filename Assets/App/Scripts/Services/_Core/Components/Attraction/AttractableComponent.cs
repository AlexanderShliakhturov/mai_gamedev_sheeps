using System;
using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Attraction.Scan;
using App.Scripts.Services._Core.Components.Movement;
using App.Scripts.Services._Core.Components.Scan;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Attraction
{
    [RequireComponent(typeof(ScanComponent))]
    public class AttractableComponent : ActivatableObject, IInitializable
    {
        [SerializeField] private ScanConfig scanConfig = new();
        [SerializeField] private ChaoticMovementComponent chaoticMovementComponent;
        [SerializeField] private TargetMovementComponent targetMovementComponent;

        private int _minPriority;
        private ScanComponent _scanComponent;
        private AttractionComponent _currentAttractionComponent;

        public void IncreaseMinPriority() => ++_minPriority;

        public UniTask InitAsync()
        {
            _scanComponent = GetComponent<ScanComponent>();
            
            _minPriority = 0;
            scanConfig.Id = Guid.NewGuid();
            scanConfig.ScanProcessor = new AttractableScanProcessor();
            var subscriber = _scanComponent.Subscribe(scanConfig);
            subscriber.BeforeScanned += OnBeforeScanned;
            subscriber.Scanned += OnScanned;
            
            return UniTask.CompletedTask;
        }

        private void OnBeforeScanned() => (scanConfig.ScanProcessor as AttractableScanProcessor)?.Set(_minPriority);

        private void OnScanned(Transform attraction)
        {
            if (_currentAttractionComponent && _currentAttractionComponent.transform == attraction)
                return;

            if (!attraction)
            {
                _currentAttractionComponent = null;
                chaoticMovementComponent.Activate();
                targetMovementComponent.Deactivate();
            }
            else
            {
                _currentAttractionComponent = attraction.GetComponent<AttractionComponent>();
                chaoticMovementComponent.Deactivate();
                targetMovementComponent.currentTarget = attraction.transform;
                targetMovementComponent.Activate();
            }
        }
    }
}