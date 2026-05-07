using App.Scripts.Services._Core.Components.Scan.Base;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Grab.Scan
{
    public class GrabScanProcessor : ScanProcessor
    {
        public override bool ProcessScan(Collider collider) => collider.TryGetComponent(out GrabbableComponent _);
    }
}