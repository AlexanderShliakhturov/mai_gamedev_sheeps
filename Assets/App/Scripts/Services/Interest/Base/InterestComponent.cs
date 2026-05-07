using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services.Interest.Config;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Services.Interest.Base
{
    public abstract class InterestComponent : ActivatableObject, IDependUpdatable, IInitializable
    {
        public abstract void SetUpConfig(InterestConfig config);
        public virtual void UpdateDependently(float deltaTime){}
        public virtual UniTask InitAsync() => UniTask.CompletedTask;
    }
}