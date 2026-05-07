using App.Scripts.Services._Base;
using App.Scripts.Services._Core.Components.Scan;
using App.Scripts.Services.Specializations.Components.Shooter;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Specializations.Config
{
    [CreateAssetMenu(fileName = nameof(SpecializationServiceConfig), menuName = "Configs/" + nameof(SpecializationServiceConfig))]
    public class SpecializationServiceConfig : ServiceConfig
    {
        public AssetReferenceGameObject windowReference;
        public ScanConfig healerScanConfig;
        public ShooterConfig shooterScanConfig;
    }
}