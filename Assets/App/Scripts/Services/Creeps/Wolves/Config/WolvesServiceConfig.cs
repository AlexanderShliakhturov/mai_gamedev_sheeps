using App.Scripts.Services._Base;
using App.Scripts.Services.Creeps.Wolves.Components.Spawn.Config;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Creeps.Wolves.Config
{
    [CreateAssetMenu(fileName = nameof(WolvesServiceConfig), menuName = "Configs/" + nameof(WolvesServiceConfig))]
    public class WolvesServiceConfig : ServiceConfig
    {
        public AssetReferenceGameObject wolfSpawnReference;
        public WolfSpawnInterestConfig interestConfig;
    }
}