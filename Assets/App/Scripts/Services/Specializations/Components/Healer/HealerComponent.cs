using System;
using System.Collections.Generic;
using App.Scripts.Services._Core.Components.Health;
using App.Scripts.Services._Core.Components.Scan;
using App.Scripts.Services.Specializations.Components.Base;
using App.Scripts.Services.Specializations.Components.Healer.Scan;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Specializations.Components.Healer
{
    [RequireComponent(typeof(ScanComponent))]
    public class HealerComponent : SpecializationComponent
    {
        private ScanConfig _scanConfig;
        private ScanComponent _scanComponent;

        public override UniTask InitAsync()
        {
            _scanConfig.Id = Guid.NewGuid();
            _scanConfig.ScanProcessor = new HealerScanProcessor();
            _scanComponent = GetComponent<ScanComponent>();
            var subscriber = _scanComponent.Subscribe(_scanConfig);
            subscriber.ScannedAll += OnScanned;
            return UniTask.CompletedTask;
        }

        private void OnScanned(List<Transform> obj)
        {
            foreach (var tr in obj)
                tr.GetComponent<HealthComponent>().Heal(1);
        }

        public void SetConfig(ScanConfig healerScanConfig) => _scanConfig = healerScanConfig;
    }
}