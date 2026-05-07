using System;
using App.Scripts.Services.Interest.Config;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Creeps.Wolves.Components.Spawn.Config
{
    [Serializable]
    public class WolfSpawnInterestConfig : InterestConfig
    {
        public int emitCount;
        public AssetReferenceGameObject wolfReference;
    }
}