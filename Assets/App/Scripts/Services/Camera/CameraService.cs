using App.Scripts.DI.Utils;
using App.Scripts.DI.Utils.Signals;
using App.Scripts.Services._Base;
using App.Scripts.Services.Camera.Config;
using App.Scripts.Services.Player.Signals;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Camera
{
    public class CameraService : IGameService
    {
        private CameraServiceConfig _config;
        public CinemachineCamera Camera { get; private set; }

        public UniTask InitAsync()
        {
            SignalManager.Subscribe<PlayerSpawnedSignal>(AssignCamera);
            return UniTask.CompletedTask;
        }

        public async UniTask StartAsync()
        {
            _config = ServiceLocator.GetConfig<CameraServiceConfig>();
            Camera = (await Addressables.InstantiateAsync(_config.cameraReference)).GetComponent<CinemachineCamera>();
        }

        private async UniTask AssignCamera(Signal arg)
        {
            var target = (arg as PlayerSpawnedSignal)?.PlayerComponent.transform;
            Camera.Target = new CameraTarget
            {
                TrackingTarget = target,
                LookAtTarget = null,
                CustomLookAtTarget = true
            };

            await UniTask.WaitForSeconds(2f);

            Camera.transform.LookAt(target);
        }
    }
}