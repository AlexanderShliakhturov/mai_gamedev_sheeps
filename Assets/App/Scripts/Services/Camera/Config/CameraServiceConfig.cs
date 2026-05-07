using App.Scripts.Services._Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Camera.Config
{
    [CreateAssetMenu(fileName = nameof(CameraServiceConfig), menuName = "Configs/" + nameof(CameraServiceConfig))]
    public class CameraServiceConfig : ServiceConfig
    {
        public AssetReferenceGameObject cameraReference;
    }
}