using System.Collections.Generic;
using App.Scripts.DI.Utils;
using App.Scripts.DI.Utils.Signals;
using App.Scripts.Services._Core.Components.Grab;
using App.Scripts.Services.Creeps.Base;
using App.Scripts.Services.Creeps.Sheeps.Components;
using App.Scripts.Services.Creeps.Sheeps.Components.Stats;
using App.Scripts.Services.Creeps.Sheeps.Components.Yard;
using App.Scripts.Services.Creeps.Sheeps.Config;
using App.Scripts.Services.Interest.Base;
using App.Scripts.Services.Interest.Components;
using App.Scripts.Services.Player.Signals;
using App.Scripts.Services.Update;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Creeps.Sheeps
{
    public class SheepsService : CreepServiceBase
    {
        public override InterestType InterestType => InterestType.Sheeps;

        private HashSet<SheepComponent> _sheeps;

        private SheepServiceConfig _config;
        private StatsWindow _statsWindow;

        public override UniTask InitAsync()
        {
            _sheeps = new HashSet<SheepComponent>();
            SignalManager.Subscribe<PlayerSpawnedSignal>(OnPlayerSpawned);
            return UniTask.CompletedTask;
        }

        private async UniTask OnPlayerSpawned(Signal arg)
        {
            _config = ServiceLocator.GetConfig<SheepServiceConfig>();
            _statsWindow =
                (await Addressables.InstantiateAsync(_config.statsWindowReference)).GetComponent<StatsWindow>();
            ((PlayerSpawnedSignal)arg).PlayerComponent.GetComponent<GrabComponent>().Grabbed +=
                _statsWindow.ChangeStats;

            ServiceLocator.GetService<SimpleUpdateService>().Add(_statsWindow);
        }
        

        public override async UniTask CreateInterestAtPoint(InterestPointComponent component)
        {
            var yard = (await Addressables.InstantiateAsync(_config.sheepYardReference))
                .GetComponent<SheepYardComponent>();

            component.AssignInterest(yard, InterestType.Sheeps);

            yard.SetUpConfig(_config.interestConfig);
            await yard.InitAsync();

            foreach (var sheep in yard.Sheeps)
                _sheeps.Add(sheep);
        }

        public override UniTask ActivateInterestAtPoint(InterestPointComponent component)
        {
            var yard = (SheepYardComponent)component.InterestComponent;

            yard.gameObject.SetActive(true);

            yard.Activate();

            return UniTask.CompletedTask;
        }

        public override UniTask DeactivateInterestAtPoint(InterestPointComponent component)
        {
            var yard = (SheepYardComponent)component.InterestComponent;

            yard.Deactivate();

            yard.gameObject.SetActive(false);

            return UniTask.CompletedTask;
        }
    }
}