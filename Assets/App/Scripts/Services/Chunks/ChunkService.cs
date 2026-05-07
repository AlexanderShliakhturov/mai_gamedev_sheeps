using System.Collections.Generic;
using App.Scripts.DI.Utils;
using App.Scripts.DI.Utils.Signals;
using App.Scripts.Services._Base;
using App.Scripts.Services.Chunks.Components;
using App.Scripts.Services.Chunks.Configs;
using App.Scripts.Services.Chunks.Signals;
using App.Scripts.Services.Player.Signals;
using App.Scripts.Services.Update;
using App.Scripts.Utils;
using App.Scripts.Utils.EditorUtils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace App.Scripts.Services.Chunks
{
    public class ChunkService : IGameService
    {
        private Transform _target;
        private GameTimer _spawnTimer;
        private ChunkServiceConfig _config;
        private Dictionary<Vector3, ChunkComponent> _spawnedChunks;

        public UniTask InitAsync()
        {
            _spawnedChunks = new();
            SignalManager.Subscribe<PlayerSpawnedSignal>(OnPlayerSpawned);
            return UniTask.CompletedTask;
        }

        public UniTask PostInitAsync()
        {
            _config = ServiceLocator.GetConfig<ChunkServiceConfig>();
            return UniTask.CompletedTask;
        }

        private UniTask OnPlayerSpawned(Signal arg)
        {
            _target = (arg as PlayerSpawnedSignal)?.PlayerComponent.transform;
            BindSpawning();
            return UniTask.CompletedTask;
        }

        private void BindSpawning()
        {
            _spawnTimer = new GameTimer();
            ServiceLocator.GetService<SimpleUpdateService>().Add(_spawnTimer);
            _spawnTimer.UseAutoRestart().Start(_config.updateChunksStateInterval);
            _spawnTimer.Completed += UpdateChunks;
        }

        private void UpdateChunks()
        {
            if (!_target)
            {
                Log.Error("no target. cannot update chunks");
                return;
            }

            for (var i = -2; i < 3; ++i)
            {
                for (var j = -2; j < 3; ++j)
                {
                    var direction = new Vector3(i, 0, j);
                    var currentPosition =
                        direction * _config.chunkDimensionLength +
                        new Vector3(_target.position.x, 0, _target.position.z);

                    var passedDeltaX = currentPosition.x % _config.chunkDimensionLength;
                    var passedDeltaZ = currentPosition.z % _config.chunkDimensionLength;

                    currentPosition = new Vector3(
                        currentPosition.x - passedDeltaX,
                        0,
                        currentPosition.z - passedDeltaZ);

                    if (!GetClosestChunkToPoint(currentPosition, out var chunk))
                    {
                        CreateRandomChunk(currentPosition).Forget();
                        continue;
                    }

                    if (chunk.Activated)
                    {
                        chunk.DecreaseAge();
                        continue;
                    }

                    ActivateChunk(chunk);
                }
            }

            foreach (var chunk in _spawnedChunks.Values)
            {
                if (!chunk.Activated)
                    continue;

                chunk.ScanEntities();
                chunk.IncreaseAge();

                if (chunk.Age > _config.chunkActiveMaxAge)
                {
                    DeactivateChunk(chunk);
                }
            }
        }

        private bool GetClosestChunkToPoint(Vector3 currentPosition, out ChunkComponent chunk)
        {
            if (!_spawnedChunks.TryGetValue(currentPosition, out var ch))
            {
                chunk = null;
                return false;
            }

            chunk = ch;
            return true;
        }

        private async UniTask CreateRandomChunk(Vector3 currentPosition)
        {
            var chunkReference = _config.chunkVariants[Random.Range(0, _config.chunkVariants.Count)];

            var newChunk = (await Addressables.InstantiateAsync(chunkReference)).GetComponent<ChunkComponent>();

            newChunk.transform.position = currentPosition;

            SignalManager.Push(new ChunkSpawnedSignal { ChunkComponent = newChunk });
            newChunk.Activate();

            _spawnedChunks.Add(currentPosition, newChunk);
        }

        private void ActivateChunk(ChunkComponent chunk)
        {
            chunk.gameObject.SetActive(true);
            chunk.Activate();
            SignalManager.Push(new ChunkActivatedSignal { ChunkComponent = chunk });
        }

        private void DeactivateChunk(ChunkComponent chunk)
        {
            chunk.Deactivate();
            SignalManager.Push(new ChunkDeactivatedSignal { ChunkComponent = chunk });
            chunk.gameObject.SetActive(false);
        }
    }
}