using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services.Entities.Components.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Health
{
    public class HealthComponent : ActivatableObject, IInitializable
    {
        [SerializeField] private int startPoints;

        public int CurrentPoints { get; private set; }

        public UniTask InitAsync()
        {
            CurrentPoints = startPoints;
            return UniTask.CompletedTask;
        }

        public void Damage(int amount)
        {
            CurrentPoints -= amount;

            if (CurrentPoints > 0)
                return;

            CurrentPoints = 0;
            GetComponent<Entity>().Deactivate();
            Destroy(gameObject);
        }

        public void Heal(int amount)
        {
            CurrentPoints += amount;
            
            if (CurrentPoints > startPoints)
                CurrentPoints = startPoints;
        }
    }
}