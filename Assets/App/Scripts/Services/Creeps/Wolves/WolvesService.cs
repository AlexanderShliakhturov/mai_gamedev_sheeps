using System.Collections.Generic;
using App.Scripts.DI.Utils;
using App.Scripts.Services.Creeps.Base;
using App.Scripts.Services.Creeps.Wolves.Components;
using App.Scripts.Services.Creeps.Wolves.Components.Spawn;
using App.Scripts.Services.Creeps.Wolves.Config;
using App.Scripts.Services.Interest.Base;
using App.Scripts.Services.Interest.Components;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Creeps.Wolves
{
    public class WolvesService : CreepServiceBase
    {
        public override InterestType InterestType => InterestType.Wolves;

        private HashSet<WolfComponent> _wolves;

        private WolvesServiceConfig _config;

        public override UniTask InitAsync()
        {
            _wolves = new HashSet<WolfComponent>();
            return UniTask.CompletedTask;
        }

        public override UniTask PostInitAsync()
        {
            _config = ServiceLocator.GetConfig<WolvesServiceConfig>();
            return UniTask.CompletedTask;
        }

        public override async UniTask CreateInterestAtPoint(InterestPointComponent component)
        {
            var spawn = (await Addressables.InstantiateAsync(_config.wolfSpawnReference))
                .GetComponent<WolfSpawnComponent>();

            component.AssignInterest(spawn, InterestType.Wolves);

            spawn.SetUpConfig(_config.interestConfig);
            await spawn.InitAsync();

            foreach (var wolf in spawn.Wolves)
                _wolves.Add(wolf);
        }

        public override UniTask ActivateInterestAtPoint(InterestPointComponent component)
        {
            var spawn = (WolfSpawnComponent)component.InterestComponent;

            spawn.gameObject.SetActive(true);

            spawn.Activate();

            return UniTask.CompletedTask;
        }

        public override UniTask DeactivateInterestAtPoint(InterestPointComponent component)
        {
            var spawn = (WolfSpawnComponent)component.InterestComponent;

            spawn.Deactivate();

            spawn.gameObject.SetActive(false);

            return UniTask.CompletedTask;
        }
    }
}