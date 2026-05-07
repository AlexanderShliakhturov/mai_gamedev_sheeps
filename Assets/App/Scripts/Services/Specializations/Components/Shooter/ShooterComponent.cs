using System;
using App.Scripts.Services._Core.Components.Movement;
using App.Scripts.Services._Core.Components.Scan;
using App.Scripts.Services.Specializations.Components.Base;
using App.Scripts.Services.Specializations.Components.Shooter.Components;
using App.Scripts.Services.Specializations.Components.Shooter.Scan;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Specializations.Components.Shooter
{
    [RequireComponent(typeof(ScanComponent))]
    public class ShooterComponent : SpecializationComponent
    {
        [SerializeField] private ShooterConfig shooterConfig;
        private ScanComponent _scanComponent;

        public override UniTask InitAsync()
        {
            shooterConfig.shooterScanConfig.Id = Guid.NewGuid();

            shooterConfig.shooterScanConfig.ScanProcessor = new ShooterScanProcessor(shooterConfig.enemyType);

            _scanComponent = GetComponent<ScanComponent>();
            var subscriber = _scanComponent.Subscribe(shooterConfig.shooterScanConfig);
            subscriber.ScannedAsync += OnScannedAsync;
            return UniTask.CompletedTask;
        }

        private async UniTask OnScannedAsync(Transform arg)
        {
            if (!arg)
                return;
            GetComponent<TargetMovementComponent>().currentTarget = arg;
            var bullet = (await Addressables.InstantiateAsync(shooterConfig.bulletReference)).GetComponent<Bullet>();
            bullet.transform.position = transform.position + transform.forward; // TODO убрать и сделать из конфига
            await bullet.InitAsync();
            bullet.Fire(arg.position);
        }

        public void SetConfig(ShooterConfig config) => shooterConfig = config;
    }

    [Serializable]
    public class
        ShooterConfig // TODO надо вынести шутера в префаб и там положить этот конфиг, а не доставать из сервиса. сейчас просто из addcomponent получается компонент шутера
    {
        public ScanConfig shooterScanConfig;
        public EnemyType enemyType;
        public AssetReferenceGameObject bulletReference;
    }

    public enum EnemyType
    {
        Wolf,
        Sheep
    }
}