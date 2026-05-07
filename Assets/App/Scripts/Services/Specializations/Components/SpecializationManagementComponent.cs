using System;
using App.Scripts.DI.Utils;
using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services.Specializations.Components.Base;
using App.Scripts.Services.Specializations.Components.Healer;
using App.Scripts.Services.Specializations.Components.Shooter;
using App.Scripts.Services.Specializations.Config;

namespace App.Scripts.Services.Specializations.Components
{
    public class SpecializationManagementComponent : ActivatableObject, IInitializable
    {
        public SpecializationComponent Current { get; private set; }
        public int ChooseCost { get; private set; } = 1;
        public int Points { get; private set; }

        public void TryChooseSpecialization()
        {
            if (Points < ChooseCost)
                return;
            Points -= ChooseCost;
            ServiceLocator.GetService<SpecializationService>().ChooseSpecialization(this);
        }

        public SpecializationComponent SetSpecialization(SpecializationType type)
        {
            if (Current)
                return Current;

            switch (type)
            {
                case SpecializationType.Leader:
                    Current = gameObject.AddComponent<LeaderComponent>();
                    break;
                case SpecializationType.Healer:
                    Current = gameObject.AddComponent<HealerComponent>();
                    ((HealerComponent)Current).SetConfig(ServiceLocator.GetConfig<SpecializationServiceConfig>().healerScanConfig);
                    break;
                case SpecializationType.Shooter:
                    Current = gameObject.AddComponent<ShooterComponent>();
                    ((ShooterComponent)Current).SetConfig(ServiceLocator.GetConfig<SpecializationServiceConfig>().shooterScanConfig);
                    break;
                case SpecializationType.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return Current;
        }

        public void AddPoints(int i) => Points += i;

        public void TryIncreaseLevel()
        {
            if (!Current || Points <= Current.IncreaseCost) 
                return;
            
            Current.IncreaseLevel();
            Points -= Current.IncreaseCost;
        }
    }
}