using System.Collections.Generic;
using System.Linq;
using App.Scripts.DI.Utils;
using App.Scripts.DI.Utils.Signals;
using App.Scripts.Services._Base;
using App.Scripts.Services.Chunks.Signals;
using App.Scripts.Services.Interest.Base;
using App.Scripts.Services.Interest.Components;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace App.Scripts.Services.Interest
{
    public class InterestPointsService : IGameService
    {
        private Dictionary<InterestType, IInterestPointService> _interestServicesWithTypes;
        private List<IInterestPointService> _interestServices;

        public UniTask InitAsync()
        {
            SignalManager.Subscribe<ChunkSpawnedSignal>(CreateInterestAtPoints);
            SignalManager.Subscribe<ChunkActivatedSignal>(ActivateInterestPoints);
            SignalManager.Subscribe<ChunkDeactivatedSignal>(DeactivatedInterestPoints);

            return UniTask.CompletedTask;
        }

        public UniTask PostInitAsync()
        {
            _interestServicesWithTypes = ServiceLocator.GetServices<IInterestPointService>()
                .ToDictionary(
                    a => a.InterestType,
                    service => service);

            _interestServices = _interestServicesWithTypes.Values.ToList();

            return UniTask.CompletedTask;
        }

        private async UniTask CreateInterestAtPoints(Signal arg)
        {
            var interestPoints = (arg as ChunkSpawnedSignal)?.ChunkComponent
                .GetComponentsInChildren<InterestPointComponent>();

            if (interestPoints == null || interestPoints.Length == 0)
                return;

            foreach (var interestPoint in interestPoints)
            {
                var service = _interestServices[Random.Range(0, _interestServices.Count)];

                await service.CreateInterestAtPoint(interestPoint);
                await service.ActivateInterestAtPoint(interestPoint);
            }
        }

        private async UniTask ActivateInterestPoints(Signal arg)
        {
            var interestPoints = (arg as ChunkActivatedSignal)?.ChunkComponent
                .GetComponentsInChildren<InterestPointComponent>();

            if (interestPoints == null || interestPoints.Length == 0)
                return;

            foreach (var interestPoint in interestPoints)
            {
                var service = _interestServicesWithTypes[interestPoint.InterestType];
                await service.ActivateInterestAtPoint(interestPoint);
            }
        }

        private async UniTask DeactivatedInterestPoints(Signal arg)
        {
            var interestPoints = (arg as ChunkDeactivatedSignal)?.ChunkComponent
                .GetComponentsInChildren<InterestPointComponent>();

            if (interestPoints == null || interestPoints.Length == 0)
                return;

            foreach (var interestPoint in interestPoints)
            {
                var service = _interestServicesWithTypes[interestPoint.InterestType];
                await service.DeactivateInterestAtPoint(interestPoint);
            }
        }
    }
}