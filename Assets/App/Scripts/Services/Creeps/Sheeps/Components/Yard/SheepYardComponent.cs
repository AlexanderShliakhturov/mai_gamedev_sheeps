using System.Collections.Generic;
using App.Scripts.DI.Utils;
using App.Scripts.Services.Creeps.Sheeps.Components.Yard.Config;
using App.Scripts.Services.Entities;
using App.Scripts.Services.Interest.Base;
using App.Scripts.Services.Interest.Config;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Creeps.Sheeps.Components.Yard
{
    public class SheepYardComponent : InterestComponent
    {
        public List<SheepComponent> Sheeps { get; private set; }
        private SheepYardInterestConfig _config;

        public override void SetUpConfig(InterestConfig config) => _config = (SheepYardInterestConfig)config;

        public override async UniTask InitAsync()
        {
            Sheeps = new List<SheepComponent>();
            for (var i = 0; i < _config.emitCount; i++)
            {
                var sheep = (await ServiceLocator.GetService<EntityService>().SpawnEntityAsync(_config.sheepReference))
                    .GetComponent<SheepComponent>();

                sheep.transform.localPosition = transform.position + Vector3.up * 2;

                await sheep.InitAsync();
                sheep.Activate();

                Sheeps.Add(sheep);
            }
        }
    }
}