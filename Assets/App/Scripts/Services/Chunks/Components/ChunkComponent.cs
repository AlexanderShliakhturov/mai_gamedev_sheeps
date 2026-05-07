using System;
using System.Collections.Generic;
using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services.Entities.Components.Base;
using UnityEngine;

namespace App.Scripts.Services.Chunks.Components
{
    public class ChunkComponent : ActivatableObject
    {
        [field: SerializeField] public int Age { get; private set; }

        private readonly HashSet<Entity> _ownedEntities = new();

        public void DecreaseAge()
        {
            if (Age - 1 < 0)
                return;

            --Age;
        }

        public void IncreaseAge() => ++Age;

        protected override void ActivateInternal()
        {
            Age = 0;

            foreach (var obj in _ownedEntities)
            {
                try
                {
                    obj.gameObject.SetActive(true);
                    obj.Activate();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        protected override void DeactivateInternal()
        {
            foreach (var obj in _ownedEntities)
            {
                try
                {
                    obj.Deactivate();
                    obj.gameObject.SetActive(false);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        public void ScanEntities()
        {
            _ownedEntities.RemoveWhere(a => a.Chunk != this);

            var colls = Physics.OverlapBox(transform.position, new Vector3(8, 2, 8));
            foreach (var coll in colls)
            {
                if (!coll.gameObject.TryGetComponent(out Entity obj))
                    continue;

                _ownedEntities.Add(obj);

                obj.Chunk = this;
            }
        }
    }
}