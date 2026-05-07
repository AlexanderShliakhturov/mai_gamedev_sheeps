using System.Collections.Generic;
using System.Linq;
using App.Scripts.DI.Utils;
using App.Scripts.Services._Base;
using App.Scripts.Services.Entities.Components.Base;
using App.Scripts.Services.Entities.Signals;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Entities
{
    public class EntityService : IGameService, IUpdateService
    {
        private HashSet<Entity> _entities;
        public IReadOnlyList<Entity> Entities => _entities.ToList();

        public UniTask InitAsync()
        {
            _entities = new HashSet<Entity>();

            return UniTask.CompletedTask;
        }

        public async UniTask<Entity> SpawnEntityAsync(AssetReferenceGameObject reference)
        {
            var entity = (await Addressables.InstantiateAsync(reference)).GetComponent<Entity>();

            await entity.InitAsync();
            
            _entities.Add(entity);

            SignalManager.Push(new EntitySpawnedSignal { Entity = entity });

            return entity;
        }

        public void Update(float deltaTime)
        {
            foreach (var entity in _entities) 
                entity.UpdateDependently(deltaTime);
        }
    }
}