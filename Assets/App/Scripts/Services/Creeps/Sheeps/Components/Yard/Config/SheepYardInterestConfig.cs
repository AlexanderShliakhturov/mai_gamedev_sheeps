
using System;
using App.Scripts.Services.Interest.Config;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Creeps.Sheeps.Components.Yard.Config
{
    [Serializable]
    public class SheepYardInterestConfig : InterestConfig
    {
        public int emitCount;
        public AssetReferenceGameObject sheepReference;
    }
}