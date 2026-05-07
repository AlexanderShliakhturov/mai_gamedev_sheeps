using App.Scripts.DI.Utils;
using App.Scripts.Services._Base;
using App.Scripts.Services.Entities.Components.Base;
using App.Scripts.Services.Specializations.Components;
using App.Scripts.Services.Specializations.Config;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Specializations
{
    public class SpecializationService : IGameService
    {
        private SpecializationWindow _specializationWindow;
        private SpecializationServiceConfig _config;
        private SpecializationManagementComponent _currentCandidate;

        public UniTask PostInitAsync()
        {
            _config = ServiceLocator.GetConfig<SpecializationServiceConfig>();
            return UniTask.CompletedTask;
        }

        public async UniTask StartAsync()
        {
            _specializationWindow = (await Addressables.InstantiateAsync(_config.windowReference))
                .GetComponent<SpecializationWindow>();

            _specializationWindow.leaderButton.onClick.AddListener(() =>
                SetSpecialization(SpecializationType.Leader));
            _specializationWindow.healerButton.onClick.AddListener(() =>
                SetSpecialization(SpecializationType.Healer));
            _specializationWindow.shooterButton.onClick.AddListener(() =>
                SetSpecialization(SpecializationType.Shooter));

            _specializationWindow.gameObject.SetActive(false);
        }

        public void ChooseSpecialization(SpecializationManagementComponent specialization)
        {
            _specializationWindow.gameObject.SetActive(true);

            _currentCandidate = specialization;
        }

        private void SetSpecialization(SpecializationType type)
        {
            if (!_currentCandidate)
                return;

            var specializationComponent = _currentCandidate.SetSpecialization(type);
            
            specializationComponent.InitAsync();
            specializationComponent.Activate();
            
            _currentCandidate.GetComponent<Entity>().AddUpdatable(_currentCandidate.Current);
            _specializationWindow.gameObject.SetActive(false);
        }
    }

    public enum SpecializationType
    {
        None,
        Leader,
        Healer,
        Shooter
    }
}