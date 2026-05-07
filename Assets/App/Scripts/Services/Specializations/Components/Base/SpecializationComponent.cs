using App.Scripts.Services._Core.Components._Base;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Services.Specializations.Components.Base
{
    public abstract class SpecializationComponent : ActivatableObject, IDependUpdatable, IInitializable
    {
        public int Level { get; private set; }
        public int IncreaseCost => 3;

        public void IncreaseLevel()
        {
            ++Level;
            IncreaseLevelInternal();
        }

        public virtual UniTask InitAsync()
        {
            return UniTask.CompletedTask;
        }

        public virtual void IncreaseLevelInternal()
        {
        }

        public virtual void UpdateDependently(float deltaTime)
        {
        }
    }
}