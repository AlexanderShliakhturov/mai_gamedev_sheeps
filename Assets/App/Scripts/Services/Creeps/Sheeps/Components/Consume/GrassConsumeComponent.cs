using System;
using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Scan;
using App.Scripts.Services.Creeps.Sheeps.Components.Consume.Scan;
using App.Scripts.Services.Entities.Components.Base;
using App.Scripts.Services.Specializations.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Creeps.Sheeps.Components.Consume
{
    [RequireComponent(typeof(ScanComponent), typeof(SpecializationManagementComponent))]
    public class GrassConsumeComponent : ActivatableObject, IInitializable
    {
        [SerializeField] private ScanConfig scanConfig;
        private ScanComponent _scanComponent;
        private SpecializationManagementComponent _specialization;

        public UniTask InitAsync()
        {
            _specialization = GetComponent<SpecializationManagementComponent>();
            _scanComponent = GetComponent<ScanComponent>();
            
            scanConfig.Id = Guid.NewGuid();
            scanConfig.ScanProcessor = new GrassConsumeScanProcessor();
            var subscriber = _scanComponent.Subscribe(scanConfig);
            subscriber.Scanned += TryConsume;
            return UniTask.CompletedTask;
        }

        private void TryConsume(Transform obj)
        {
            if (!obj)
                return;
                
            obj.GetComponent<Entity>().Deactivate();
            Destroy(obj.gameObject);
            _specialization.AddPoints(1);
        }

    }
}