using App.Scripts.Services._Base;
using App.Scripts.Services.Grass.Components.GrassPlane.Config;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Grass.Config
{
    [CreateAssetMenu(fileName = nameof(GrassServiceConfig), menuName = "Configs/" + nameof(GrassServiceConfig))]
    public class GrassServiceConfig : ServiceConfig
    {
        public AssetReferenceGameObject grassPlaneReference;
        public GrassPlaneConfig grassPlaneConfig;
    }
}