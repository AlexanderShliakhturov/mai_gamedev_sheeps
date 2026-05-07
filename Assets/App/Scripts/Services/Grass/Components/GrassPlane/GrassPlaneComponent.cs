using App.Scripts.DI.Utils;
using App.Scripts.Services.Chunks.Configs;
using App.Scripts.Services.Entities;
using App.Scripts.Services.Grass.Components.GrassPlane.Config;
using App.Scripts.Services.Interest.Base;
using App.Scripts.Services.Interest.Config;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Grass.Components.GrassPlane
{
    public class GrassPlaneComponent : InterestComponent
    {
        private GrassPlaneConfig _config;

        public override void SetUpConfig(InterestConfig config) => _config = (GrassPlaneConfig)config;

        public override async UniTask InitAsync()
        {
            var length = ServiceLocator.GetConfig<ChunkServiceConfig>().chunkDimensionLength;
            for (var i = 0; i < _config.emitCount; i++)
            {
                var grass = (await ServiceLocator.GetService<EntityService>().SpawnEntityAsync(_config.grassReference))
                    .GetComponent<GrassComponent>();
                
                grass.transform.position = transform.position + new Vector3(
                    Random.Range(-length / 2, length / 2), 
                    0.25f,
                    Random.Range(-length / 2, length / 2));
                
                grass.Activate();
            }
        }
    }
}