using App.Scripts.Services._Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Player.Config
{
    [CreateAssetMenu(fileName = nameof(PlayerServiceConfig), menuName = "Configs/" + nameof(PlayerServiceConfig))]
    public class PlayerServiceConfig : ServiceConfig
    {
        public AssetReferenceGameObject playerReference;
    }
}