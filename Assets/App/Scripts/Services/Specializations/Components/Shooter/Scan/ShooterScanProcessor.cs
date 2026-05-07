using App.Scripts.Services._Core.Components.Scan.Base;
using App.Scripts.Services.Creeps.Sheeps.Components;
using App.Scripts.Services.Creeps.Wolves.Components;
using UnityEngine;

namespace App.Scripts.Services.Specializations.Components.Shooter.Scan
{
    public class ShooterScanProcessor : ScanProcessor
    {
        private readonly EnemyType _type;
        
        public ShooterScanProcessor(EnemyType type) => _type = type;

        public override bool ProcessScan(Collider collider)
        {
            return _type switch
            {
                EnemyType.Sheep => collider.TryGetComponent(out SheepComponent _),
                EnemyType.Wolf => collider.TryGetComponent(out WolfComponent _),
                _ => false
            };
        }
    }
}