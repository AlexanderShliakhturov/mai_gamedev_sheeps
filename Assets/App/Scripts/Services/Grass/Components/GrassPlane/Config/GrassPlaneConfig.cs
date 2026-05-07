using System;
using App.Scripts.Services.Interest.Config;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Grass.Components.GrassPlane.Config
{
    [Serializable]
    public class GrassPlaneConfig : InterestConfig
    {
        public int emitCount;
        public AssetReferenceGameObject grassReference;
    }
}