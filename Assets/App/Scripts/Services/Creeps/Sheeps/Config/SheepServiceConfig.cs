using App.Scripts.Services._Base;
using App.Scripts.Services.Creeps.Sheeps.Components.Yard.Config;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Creeps.Sheeps.Config
{
    [CreateAssetMenu(fileName = nameof(SheepServiceConfig),  menuName = "Configs/" + nameof(SheepServiceConfig))]
    public class SheepServiceConfig : ServiceConfig
    {
        public AssetReferenceGameObject sheepYardReference;
        public SheepYardInterestConfig interestConfig;
        public AssetReferenceGameObject statsWindowReference;
    }
}