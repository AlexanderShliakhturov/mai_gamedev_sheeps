using App.Scripts.Services._Core.Components.Health;
using App.Scripts.Services._Core.Components.Scan.Base;
using UnityEngine;

namespace App.Scripts.Services.Specializations.Components.Healer.Scan
{
    public class HealerScanProcessor : ScanProcessor
    {
        public override bool ProcessScan(Collider collider) => collider.TryGetComponent(out HealthComponent _);
    }
}