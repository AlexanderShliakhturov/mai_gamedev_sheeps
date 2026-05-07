using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Services._Base;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Services.Update
{
    public class SimpleUpdateService : IGameService, IUpdateService
    {
        private Dictionary<Guid, IUpdatable> _updatables;
        private List<IUpdatable> _updatablesList;

        public UniTask InitAsync()
        {
            _updatables = new Dictionary<Guid, IUpdatable>();

            return UniTask.CompletedTask;
        }

        public Guid Add(IUpdatable updateable)
        {
            var guid = Guid.NewGuid();
            _updatables.Add(guid, updateable);

            _updatablesList = _updatables.Values.ToList();

            return guid;
        }

        public bool Remove(Guid id) => _updatables.Remove(id);

        public void Update(float deltaTime)
        {
            if (_updatablesList == null)
                return;

            foreach (var updatable in _updatablesList)
            {
                if (updatable.IsPaused)
                    continue;

                updatable.Tick(deltaTime);
            }
        }
    }
}