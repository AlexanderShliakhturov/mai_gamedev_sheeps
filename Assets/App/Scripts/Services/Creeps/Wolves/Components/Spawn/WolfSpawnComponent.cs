using System.Collections.Generic;
using App.Scripts.DI.Utils;
using App.Scripts.Services.Creeps.Wolves.Components.Spawn.Config;
using App.Scripts.Services.Entities;
using App.Scripts.Services.Interest.Base;
using App.Scripts.Services.Interest.Config;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Creeps.Wolves.Components.Spawn
{
    public class WolfSpawnComponent : InterestComponent
    {
        public List<WolfComponent> Wolves { get; private set; }
        private WolfSpawnInterestConfig _config;

        public override void SetUpConfig(InterestConfig config) => _config = (WolfSpawnInterestConfig)config;

        public override async UniTask InitAsync()
        {
            Wolves = new List<WolfComponent>();
            for (var i = 0; i < _config.emitCount; i++)
            {
                var wolf = (await ServiceLocator.GetService<EntityService>().SpawnEntityAsync(_config.wolfReference))
                    .GetComponent<WolfComponent>();

                wolf.transform.localPosition = transform.position + Vector3.up * 2;

                await wolf.InitAsync();
                wolf.Activate();

                Wolves.Add(wolf);
            }
        }
    }
}