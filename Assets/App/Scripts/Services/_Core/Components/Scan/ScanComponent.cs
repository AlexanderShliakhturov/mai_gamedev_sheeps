using System;
using System.Collections.Generic;
using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Scan.Base;
using App.Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Scan
{
    public class ScanComponent : ActivatableObject, IDependUpdatable, IInitializable
    {
        private Dictionary<Guid, ScanSubscriber> _configs;
        private Dictionary<Guid, GameTimer> _timers;
        private Collider[] _collidersBuffer;

        public UniTask InitAsync()
        {
            _collidersBuffer = new Collider[20];
            _timers ??= new Dictionary<Guid, GameTimer>();
            _configs ??= new Dictionary<Guid, ScanSubscriber>();
            return UniTask.CompletedTask;
        }

        public ScanSubscriber Subscribe(ScanConfig config)
        {
            _timers ??= new Dictionary<Guid, GameTimer>();
            _configs ??= new Dictionary<Guid, ScanSubscriber>();

            var subscriber = new ScanSubscriber
            {
                ScanConfig = config,
            };

            _configs.TryAdd(config.Id, subscriber);

            var timer = new GameTimer().UseAutoRestart();
            timer.Start(config.frequency);
            timer.Completed += () => ScanInternal(config.Id);

            _timers.TryAdd(config.Id, timer);

            return subscriber;
        }

        public void UpdateDependently(float deltaTime)
        {
            if (!Activated)
                return;
            
            foreach (var timer in _timers)
                timer.Value.Tick(deltaTime);
        }

        private void ScanInternal(Guid configId)
        {
            ClearBuffer();
            var subscriber = _configs[configId];
            subscriber.InvokeBeforeScanned();
            var radius = subscriber.ScanConfig.radius;
            var processor = subscriber.ScanConfig.ScanProcessor;

            Physics.OverlapSphereNonAlloc(
                subscriber.ScanConfig?.scanPoint ? subscriber.ScanConfig.scanPoint.position : transform.position,
                radius,
                _collidersBuffer);

            if (subscriber.ScanConfig!.scanAll)
            {
                subscriber.InvokeScannedAll(ScanAll(processor));
            }

            if (!subscriber.ScanConfig.scanAll)
            {
                var nearest = ScanNearest(processor);
                if (subscriber.ScanConfig.scanAsync)
                    subscriber.InvokeScannedAsync(nearest);
                else
                    subscriber.InvokeScanned(nearest);
            }


            processor.Reset();
        }

        private List<Transform> ScanAll(ScanProcessor processor)
        {
            var transforms = new List<Transform>();
            foreach (var coll in _collidersBuffer)
            {
                if (!coll)
                    continue;

                if (coll.gameObject == gameObject)
                    continue;

                if (!processor.ProcessScan(coll))
                    continue;

                transforms.Add(coll.transform);
            }

            return transforms;
        }

        private Transform ScanNearest(ScanProcessor processor)
        {
            var minDistance = float.MaxValue;
            Transform nearest = null;
            foreach (var coll in _collidersBuffer)
            {
                if (!coll)
                    continue;

                if (coll.gameObject == gameObject)
                    continue;

                if (!processor.ProcessScan(coll))
                    continue;

                var distance = (transform.position - coll.transform.position).magnitude;

                if (distance >= minDistance)
                    continue;

                minDistance = distance;
                nearest = coll.transform;
            }

            return nearest;
        }

        private void ClearBuffer()
        {
            for (var i = 0; i < _collidersBuffer.Length; i++)
                _collidersBuffer[i] = null;
        }
    }

    public class ScanSubscriber
    {
        public event Action<Transform> Scanned;
        public event Func<Transform, UniTask> ScannedAsync;
        public event Action<List<Transform>> ScannedAll;
        public event Action BeforeScanned;
        public ScanConfig ScanConfig;

        public void InvokeBeforeScanned() => BeforeScanned?.Invoke();
        public void InvokeScanned(Transform transform) => Scanned?.Invoke(transform);
        public void InvokeScannedAsync(Transform transform) => ScannedAsync?.Invoke(transform).Forget();
        public void InvokeScannedAll(List<Transform> transforms) => ScannedAll?.Invoke(transforms);
    }

    [Serializable]
    public class ScanConfig
    {
        public bool scanAsync = false;
        public bool scanAll = false;
        public Transform scanPoint;
        public float radius;
        public float frequency;
        public Guid Id;
        public ScanProcessor ScanProcessor;
    }
}