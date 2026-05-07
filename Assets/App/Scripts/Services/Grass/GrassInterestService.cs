using App.Scripts.DI.Utils;
using App.Scripts.Services._Base;
using App.Scripts.Services.Grass.Components.GrassPlane;
using App.Scripts.Services.Grass.Config;
using App.Scripts.Services.Interest.Base;
using App.Scripts.Services.Interest.Components;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Grass
{
    public class GrassInterestService : IGameService, IInterestPointService 
    {
        public InterestType InterestType => InterestType.Grass;
        private GrassServiceConfig _config;

        public UniTask PostInitAsync()
        {
            _config = ServiceLocator.GetConfig<GrassServiceConfig>();
            return UniTask.CompletedTask;
        }

        public async UniTask CreateInterestAtPoint(InterestPointComponent component)
        {
            var plane = (await Addressables.InstantiateAsync(_config.grassPlaneReference))
                .GetComponent<GrassPlaneComponent>();
            
            component.AssignInterest(plane, InterestType.Grass);
            
            plane.SetUpConfig(_config.grassPlaneConfig);

            await plane.InitAsync();
        }

        public UniTask ActivateInterestAtPoint(InterestPointComponent component)
        {
            var plane = (GrassPlaneComponent)component.InterestComponent;

            plane.gameObject.SetActive(true);

            plane.Activate();

            return UniTask.CompletedTask;
        }

        public UniTask DeactivateInterestAtPoint(InterestPointComponent component)
        {
            var plane = (GrassPlaneComponent)component.InterestComponent;

            plane.Deactivate();

            plane.gameObject.SetActive(false);

            return UniTask.CompletedTask;
        }
    }
}