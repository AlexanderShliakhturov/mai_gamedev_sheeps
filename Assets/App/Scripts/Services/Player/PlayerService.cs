using App.Scripts.DI.Utils;
using App.Scripts.DI.Utils.Signals;
using App.Scripts.Services._Base;
using App.Scripts.Services.Chunks.Signals;
using App.Scripts.Services.Entities;
using App.Scripts.Services.Input;
using App.Scripts.Services.Player.Components;
using App.Scripts.Services.Player.Config;
using App.Scripts.Services.Player.Signals;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Player
{
    public class PlayerService : IGameService
    {
        public PlayerComponent PlayerComponent { get; private set; }
        private EntityService _entityService;
        private PlayerServiceConfig _config;
        private InputService _inputService;

        public UniTask InitAsync()
        {
            SignalManager.Subscribe<ChunkSpawnedSignal>(ActivatePlayer);
            return UniTask.CompletedTask;
        }

        private UniTask ActivatePlayer(Signal arg)
        {
            var chunk = ((ChunkSpawnedSignal)arg).ChunkComponent;

            if (chunk.transform.position == Vector3.zero)
            {
                PlayerComponent.transform.position = chunk.transform.position + Vector3.one;
                PlayerComponent.Activate();
            }

            return UniTask.CompletedTask;
        }

        public UniTask PostInitAsync()
        {
            _entityService = ServiceLocator.GetService<EntityService>();
            _config = ServiceLocator.GetConfig<PlayerServiceConfig>();
            _inputService = ServiceLocator.GetService<InputService>();
            
            return UniTask.CompletedTask;
        }

        public async UniTask StartAsync()
        {
            PlayerComponent = (await _entityService.SpawnEntityAsync(_config.playerReference))
                .GetComponent<PlayerComponent>();
            
            PlayerComponent.AssignInput(_inputService);
            PlayerComponent.Deactivate();
            
            SignalManager.Push(new PlayerSpawnedSignal { PlayerComponent = PlayerComponent });
        }
    }
}