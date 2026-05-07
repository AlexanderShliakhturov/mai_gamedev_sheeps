using App.Scripts.Services._Core.Components.Scan.Base;
using App.Scripts.Services.Grass.Components;
using UnityEngine;

namespace App.Scripts.Services.Creeps.Sheeps.Components.Consume.Scan
{
    public class GrassConsumeScanProcessor : ScanProcessor
    {
        public override bool ProcessScan(Collider collider) => collider.TryGetComponent(out GrassComponent _);
    }
}