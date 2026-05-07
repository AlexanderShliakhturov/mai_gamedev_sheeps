using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Health;
using App.Scripts.Services.Specializations.Components;
using App.Scripts.Services.Specializations.Components.Base;
using App.Scripts.Services.Update;
using TMPro;
using UnityEngine;

namespace App.Scripts.Services.Creeps.Sheeps.Components.Stats
{
    [RequireComponent(typeof(Canvas))]
    public class StatsWindow : ActivatableObject, IInitializable, IUpdatable
    {
        public bool IsPaused { get; private set; }
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private TextMeshProUGUI specialization;
        [SerializeField] private TextMeshProUGUI points;
        private SpecializationManagementComponent _specializationManagement;
        private HealthComponent _health;
        private SpecializationComponent _specialization;


        public void ChangeStats(GameObject obj)
        {
            if (!obj)
                return;

            _health = obj.GetComponent<HealthComponent>();
            _specialization = obj.GetComponent<SpecializationComponent>();
            _specializationManagement = obj.GetComponent<SpecializationManagementComponent>();
        }

        public void Tick(float deltaTime) => UpdateStats();


        private void UpdateStats()
        {
            _specializationManagement ??= _health?.GetComponent<SpecializationManagementComponent>();
            _specialization ??= _health?.GetComponent<SpecializationComponent>();
            health.text = "Health " + _health?.CurrentPoints;
            level.text = "Level " + _specialization?.Level;
            specialization.text = "Specialization " + _specialization?.GetType().Name;
            points.text = "Points " + _specializationManagement?.Points;
        }
    }
}